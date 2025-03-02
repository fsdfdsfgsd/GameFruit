using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;//�ٶ�

    private Rigidbody2D rb;//����
    private float moveX,moveY;//��ȡ����

    private Vector2 moveDirection;
    private Animator anim;
    private float stopX,stopY;//��¼
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()//���벿��
    {
        inputSection();
    }

    private void FixedUpdate()//����
    {
        playerMove();
        playerAnim();
    }

    private void inputSection()//���벿��
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2 (moveX, moveY).normalized;
    }

    private void playerMove()//�ƶ�
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
