using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMove = 5f;

    private float moveX;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveX * speedMove,rb.velocity.y);
    }
}
