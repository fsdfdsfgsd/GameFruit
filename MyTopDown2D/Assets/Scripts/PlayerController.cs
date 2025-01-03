using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;        //�ƶ��ٶ�
    public float accelerateSpeed;      //�����ƶ��ٶ�


    private Rigidbody2D rb;             //����
    private float moveX, moveY;         //��ȡ����
    private Vector2 moveDirection;
    private Animator anim;              //����
    private float faceX, faceY;         //��¼��X��Y��ֵ
    private bool isRun;                 //�Ƿ���shift


    private enum PlayerState { idle, walk, run };    //��Ϸ״̬
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        InputSection();
    }
    private void FixedUpdate()//����
    {
        PlayerMove();
        PlayerAnim();
        PlayerRun();
    }

    private void InputSection()//���벿��
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        isRun = Input.GetKey(KeyCode.LeftShift);
        moveDirection = new Vector2 (moveX, moveY).normalized;
    }

    private void PlayerMove()//�ƶ�
    {
        rb.velocity = new Vector2(moveDirection.x*movespeed,moveDirection.y*movespeed);
    }

    private void PlayerAnim()//����
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
