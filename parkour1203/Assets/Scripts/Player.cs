using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMove = 5f;
    [Range(0f, 10f)]
    public float jumpSpeed;
    public bool isGround;
    public Transform groundCheck;
    public LayerMask ground;

    private float moveX;
    private bool jumpMove;
    private Rigidbody2D rb;
    private bool facingRight = true;
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

        if (jumpMove && isGround)
        {
            //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move(); 
        Jump();
    }
    private void Move()//ÒÆ¶¯
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

    private void Flip()//·­×ª
    {
        facingRight = !facingRight;
        Vector3 playerMove = transform.localScale;
        playerMove.x *= -1;
        transform.localScale = playerMove;
    }

    private void Jump()//ÌøÔ¾
    {
        

    }
}
