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
    public GameObject blood;

    private Rigidbody2D rb;//
    [SerializeField] private int jumpCount = 0;//��Ծ����
    private float moveX;
    private bool jumpMove;//��Ծ����
    private bool jumpHold;//������Ծ
    private bool facingRight = true;//�泯��
    private bool isJump;//�𴫵����õģ���ʾ��Ծ״̬
    private Animator anim;
    private Transform KillPoint;

    private bool canDash = true;    //�Ƿ���Գ��
    private bool isDashing;         //���ڳ����

    private float dashingPower = 25f;   //��̵�����
    private float dashingTime = 0.2f;   //��̵�ʱ��
    private float noDashTime = 1f;      //��̵���ȴʱ��
    private enum playerState{ idle,run,jump,fall,doubleJump};//��Ϸ״̬

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

    private void FixedUpdate()//����
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
        if (isGround)
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
    private void PPS()//����
    {
        playerPs.Play();
    }
    private void playerAnim()//����
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
        isDashing = true; //���ڳ���е�ʱ��

        float dashingGravity = rb.gravityScale;//��̵�ʱ�� ����������Ӱ�� ����Ϊ0
        rb.gravityScale = 0f;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = dashingGravity;
        
        isDashing = false;  //��̽���
        yield return new WaitForSeconds(noDashTime);   //������ȴʱ��
        canDash = true;
    }

}
