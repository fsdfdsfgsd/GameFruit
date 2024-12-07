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

    private Rigidbody2D rb;//
    [SerializeField] private int jumpCount = 0;//跳跃次数
    private float moveX;
    private bool jumpMove;//跳跃输入
    private bool jumpHold;//长安跳跃
    private bool facingRight = true;//面朝左
    private bool isJump;//起传递作用的，表示跳跃状态

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        jumpMove = Input.GetButtonDown("Jump");
        jumpHold = Input.GetButton("Jump");

        if (jumpMove && jumpCount>0)
        {
            isJump = true;
            jumpCount--;
            PPS();
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move(); 
        Jump();
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
        if(isGround)
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
        }

        if(rb.velocity.y < 0)
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
    private void PPS()
    {
        playerPs.Play();
    }
}
