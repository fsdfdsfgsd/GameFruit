using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;        //移动速度
    public float accelerateSpeed;      //加速移动速度


    private Rigidbody2D rb;             //刚体
    private float moveX, moveY;         //获取输入
    private Vector2 moveDirection;
    private Animator anim;              //动画
    private float faceX, faceY;         //记录用X和Y的值
    private bool isRun;                 //是否按下shift


    private enum PlayerState { idle, walk, run };    //游戏状态
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        InputSection();
    }
    private void FixedUpdate()//物理
    {
        PlayerMove();
        PlayerAnim();
        PlayerRun();
    }

    private void InputSection()//输入部分
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        isRun = Input.GetKey(KeyCode.LeftShift);
        moveDirection = new Vector2 (moveX, moveY).normalized;
    }

    private void PlayerMove()//移动
    {
        rb.velocity = new Vector2(moveDirection.x*movespeed,moveDirection.y*movespeed);
    }

    private void PlayerAnim()//动画
    {
        PlayerState state;
        if (moveDirection != Vector2.zero)
        {
            state = PlayerState.walk;
            faceX = moveX;
            faceY = moveY;
        }
        else
        {
            state = PlayerState.idle;
        }
        if (isRun && moveDirection != Vector2.zero)
        {
            state = PlayerState.run;
        }
        anim.SetFloat("inputX", faceX);
        anim.SetFloat("inputY", faceY);

        anim.SetInteger("state", (int)state);
    }

    private void PlayerRun()
    {
        if(isRun && moveDirection != Vector2.zero)
        {
            rb.velocity = new Vector2(moveDirection.x * movespeed * accelerateSpeed, moveDirection.y * movespeed * accelerateSpeed);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);
        }
    }
}
