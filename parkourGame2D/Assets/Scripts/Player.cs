using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem palyerPs;
    public float moveSpeed = 5f;
    [Range(0f, 10f)]
    public float jumpSpeed;//跳跃速读
    public bool isGrounded;//已经在地面上
    public Transform groundCheck;//检测点
    public LayerMask ground;//图层
    public float fallAddition = 3.5f;//下落的重力加成
    public float jumpAddition = 1.5f;//起跳重力加成
    public int jumpCount;


    private Rigidbody2D rb;
    private float moveX;
    private bool facingRight = true;//面朝左
    private bool moveJump;//跳跃输入
    private bool jumpHold;//长按跳跃
    private bool isJump;//起传递作用的，表示跳跃状态
    private Animator anim;
    private enum playerState { idle,run,jump,fall};

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetButtonDown("Jump");//检查按下跳跃按键
        jumpHold = Input.GetButton("Jump");


        if (moveJump && jumpCount>0)
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move();
        Jump();
        playerAnim();
    }

    private void Move()//移动
    {
        rb.velocity = new Vector2(moveX * moveSpeed,rb.velocity.y);

        if(facingRight == false && moveX > 0)
        {
            flip();
        }
        if (facingRight == true && moveX < 0)
        {
            flip();
        }
    }

    private void flip()//翻转
    {
        PPS();
        facingRight = !facingRight;
        Vector3 playerMove = transform.localScale;
        playerMove.x *= -1;
        transform.localScale = playerMove;
    }

    private void Jump()//跳跃
    {
        if(isGrounded)
        {
            jumpCount = 1;
        }

        if (isJump)
        {
            rb.AddForce(Vector2.up*jumpSpeed,ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
            PPS();
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallAddition;
        }
        else if(rb.velocity.y > 0 && !jumpHold)
        {
            rb.gravityScale = jumpAddition;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    
    void PPS()//粒子
    {
        palyerPs.Play();
    }
    void playerAnim()
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
        if (rb.velocity.y > 0.1f)
        {
            states = playerState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            states = playerState.fall;
        }
        anim.SetInteger("state", (int)states);
    }
}
