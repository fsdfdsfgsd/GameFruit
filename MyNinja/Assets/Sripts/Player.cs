using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float InputX = 0;
    public Rigidbody2D Rigidbody2D;
    public float maxForce = 100;
    public float maxVelocity = 10;
    public bool isGround = false;
    public bool isWall = false;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");

        Rigidbody2D.AddForce(new Vector2(maxForce * InputX,0));

        if(InputX > 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if(InputX < 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if(isGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxVelocity);
                isGround = false;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

}
