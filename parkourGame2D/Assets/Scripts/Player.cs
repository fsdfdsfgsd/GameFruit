using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    [Range(0f, 10f)]
    public float jumpSpeed;//��Ծ�ٶ�
    public bool isGrounded;//�Ѿ��ڵ�����
    public Transform groundCheck;//����
    public LayerMask ground;//ͼ��
    public float fallAddition = 3.5f;//����������ӳ�
    public float jumpAddition = 1.5f;//���������ӳ�
    public int jumpCount;


    private Rigidbody2D rb;
    private float moveX;
    private bool facingRight = true;//�泯��
    private bool moveJump;//��Ծ����
    private bool jumpHold;//������Ծ
    private bool isJump;//�𴫵����õģ���ʾ��Ծ״̬


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetButtonDown("Jump");//��鰴����Ծ����
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
    }

    private void Move()//�ƶ�
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

    private void flip()//��ת
    {
        facingRight = !facingRight;
        Vector3 playerMove = transform.localScale;
        playerMove.x *= -1;
        transform.localScale = playerMove;
    }

    private void Jump()
    {
        if(isGrounded)
        {
            jumpCount = 2;
        }

        if (isJump)
        {
            rb.AddForce(Vector2.up*jumpSpeed,ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;

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
}
