using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;//速度

    private Rigidbody2D rb;//刚体
    private float moveX,moveY;//获取输入

    private Vector2 moveDirection;
    private Animator anim;
    private float stopX,stopY;//记录
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()//输入部分
    {
        inputSection();
    }

    private void FixedUpdate()//物理
    {
        playerMove();
        playerAnim();
    }

    private void inputSection()//输入部分
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2 (moveX, moveY).normalized;
    }

    private void playerMove()//移动
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void playerAnim()
    {
        if(moveDirection != Vector2.zero)
        {
            anim.SetBool("isWalking",false);
            stopX = moveX;
            stopY = moveY;
        }
        else
        {
            anim.SetBool("isWalking",true);
        }
        anim.SetFloat("inputX", stopX);
        anim.SetFloat("inputY", stopY);
    }
}
