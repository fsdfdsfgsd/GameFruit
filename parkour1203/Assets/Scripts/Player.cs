using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speedMove = 5f;//移动速度
    [Range(0f, 10f)]
    public float jumpSpeed;//跳跃速度
    public bool isGround;//已经在地面上
    public Transform groundCheck;//检测点
    public LayerMask ground;//图层
    public float fallAddition = 3.5f;//下落的重力加成
    public float jumpAddition = 1.5f;//跳跃的重力加成
    public ParticleSystem playerPs;
    public GameObject blood;

    private Rigidbody2D rb;//
    [SerializeField] private int jumpCount = 0;//跳跃次数
    private float moveX;
    private bool jumpMove;//跳跃输入
    private bool jumpHold;//长安跳跃
    private bool facingRight = true;//面朝左
    private bool isJump;//起传递作用的，表示跳跃状态
    private Animator anim;
    private Transform KillPoint;

    private bool canDash = true;    //是否可以冲刺
    private bool isDashing;         //正在冲刺中

    private float dashingPower = 25f;   //冲刺的力量
    private float dashingTime = 0.2f;   //冲刺的时间
    private float noDashTime = 1f;      //冲刺的冷却时间
    private enum playerState{ idle,run,jump,fall,doubleJump};//游戏状态

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        KillPoint = transform.Find("killPoint");

    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing)
        {
            return;
        }
        moveX = Input.GetAxis("Horizontal");
        jumpMove = Input.GetButtonDown("Jump");
        jumpHold = Input.GetButton("Jump");

        if (jumpMove && jumpCount>0)
        {
            isJump = true;
            jumpCount--;
            PPS();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()//物理
    {
        if (isDashing)
        {
            return;
        }
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move(); 
        Jump();
        playerAnim();
        Enemy();
    }
    private void Move()//移动
    {
        rb.velocity = new Vector2(moveX * speedMove,rb.velocity.y);

        if(facingRight == false && moveX > 0)
        {
            Flip();
        }
        if(facingRight == true && moveX < 0)
        {
            Flip();
        }
    }

    private void Flip()//翻转
    {
        PPS();
        facingRight = !facingRight;
        Vector3 playerMove = transform.localScale;
        playerMove.x *= -1;
        transform.localScale = playerMove;
    }

    private void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 2;
        }
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);//第一个是二维向量的力，第二个是力的种类
            //力的种类 有两种
            //force是普通力
            //impulse是瞬间加上去的力
            //rb.velocity = Vector2.up * jumpSpeed;
            jumpCount--;
            isJump = false;
            FindObjectOfType<AudioMusic>().playerPlay(0);

        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallAddition;
        }
        else if (rb.velocity.y > 0 && !jumpHold)
        {
            rb.gravityScale = jumpAddition;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    private void PPS()//粒子
    {
        playerPs.Play();
    }
    private void playerAnim()//动画
    {
        playerState states;
        if (Mathf.Abs(moveX) > 0)
        {
            states = playerState.run;
        }
        else
        {
             states = playerState.idle;
        }
        if(rb.velocity.y > 0.1f)
        {
            states = playerState.jump;
        }
        else if(rb.velocity.y < -0.1f)
        {
            states = playerState.fall;
        }
        if(jumpMove || jumpHold && rb.velocity.y > 0f)
        {
            states = playerState.doubleJump;
        }

        anim.SetInteger("state", (int)states);
    }
    private void Enemy()
    {
         Collider2D enemy = Physics2D.OverlapBox(KillPoint.position, new Vector3(0.3f, 0.5f, 0f), 0f, LayerMask.GetMask("Enemy"));
        if(enemy == null)
        {
            return;
        }
        else
        {
            Instantiate(blood,transform.position,Quaternion.identity);
            Destroy(enemy.gameObject,0.3f);
        }
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        rb.AddForce(new Vector2(0f, 350f));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "springBed")
        {
            jumpCount = 0;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true; //正在冲刺中的时候

        float dashingGravity = rb.gravityScale;//冲刺的时候 不受重力的影响 让它为0
        rb.gravityScale = 0f;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = dashingGravity;
        
        isDashing = false;  //冲刺结束
        yield return new WaitForSeconds(noDashTime);   //加入冷却时间
        canDash = true;
    }

}
