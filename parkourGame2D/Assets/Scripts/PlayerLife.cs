using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public GameObject palyerPs;

    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Death();
        }
    }

    private void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(palyerPs, 1f);
        anim.SetTrigger("death");
        
        //Destroy(gameObject);
    }
}
