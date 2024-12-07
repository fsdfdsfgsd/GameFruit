using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMove = 5f;//�ƶ��ٶ�
    [Range(0f, 10f)]
    public float jumpSpeed;//��Ծ�ٶ�
    public bool isGround;//�Ѿ��ڵ�����
    public Transform groundCheck;//����
    public LayerMask ground;//ͼ��
    public float fallAddition = 3.5f;//����������ӳ�
    public float jumpAddition = 1.5f;//��Ծ�������ӳ�
    public ParticleSystem playerPs;

    private Rigidbody2D rb;//
    [SerializeField] private int jumpCount = 0;//��Ծ����
    private float moveX;
    private bool jumpMove;//��Ծ����
    private bool jumpHold;//������Ծ
    private bool facingRight = true;//�泯��
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
    private void Move()//�ƶ�
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

    private void Flip()//��ת
    {
        PPS();
        facingRight = !facingRight;
        Vector3 playerMove = transform.localScale;
        playerMove.x *= -1;
        transform.localScale = playerMove;
    }

    private void Jump()//��Ծ
    {
        if(isGround)
        {
            jumpCount = 2;
        }
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);//��һ���Ƕ�ά�����������ڶ�������������
            //�������� ������
            //force����ͨ��
            //impulse��˲�����ȥ����
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
