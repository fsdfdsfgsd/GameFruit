using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerFoce = 100;
    public Rigidbody2D rigidbody2;
    public float Horizontal;
    public float Velocity = 10;

    public bool IsGround = false;
    public bool IsOnWall = false;

    public GameObject jumpPS;
    public Transform jumpPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        rigidbody2.AddForce(new Vector2(Horizontal*playerFoce,0));
        if (Horizontal > 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (Horizontal < 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, Velocity);
                IsGround = false;

                CreateAndPlayJumpPs();
            }
        }
        
    }

    void CreateAndPlayJumpPs()
    {
        if (jumpPS == null || jumpPos == null)
        {
            return;
        }
        GameObject NewJumpPs = Instantiate(jumpPS, jumpPos.position, jumpPS.transform.rotation);

        Destroy(NewJumpPs,NewJumpPs.GetComponent<ParticleSystem>().main.duration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
        }
    }
}
