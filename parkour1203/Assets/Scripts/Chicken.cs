using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundCheckDistance, wallCheckDistance;//
    [SerializeField] private LayerMask isGround, isWall;
    [SerializeField] private Transform groundCheck, wallcheck;

    private int facingDirection = 1;
    private bool groundDetect;
    private bool wallDetect;
    private Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ChickenDetected();
        ChickenAnim();
    }

    private void ChickenAnim()//动画播放
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            anim.SetBool("isRunning",true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void ChickenDetected()//检测
    {
        groundDetect = Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,isGround);
        wallDetect = Physics2D.Raycast(wallcheck.position,transform.right, wallCheckDistance, isGround);

        if(wallDetect || !groundDetect)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
        }
    }

    private void Flip()//翻转
    {
        facingDirection = facingDirection * -1;//反转当前
        rb.transform.Rotate(0f,180f,0f);
    }

    private void OnDrawGizmos()
    {
        //
        Gizmos.DrawLine(groundCheck.position,new Vector2(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        //
        Gizmos.DrawLine(wallcheck.position, new Vector2(wallcheck.position.x + wallCheckDistance, wallcheck.position.y));

    }
}
