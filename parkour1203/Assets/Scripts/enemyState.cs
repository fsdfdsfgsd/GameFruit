using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyState : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc2D;

    private enum enemyZhuangTai{ idle,run,die };
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        enemyAinm();
    }
    private void enemyAinm()
    {
        enemyZhuangTai enemystates;
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            enemystates = enemyZhuangTai.run;
        }
        else
        {
            enemystates = enemyZhuangTai.idle;
        }
        anim.SetInteger("state",(int)enemystates);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("killpoint"))
        {
            anim.SetTrigger("die");
        }
    }
}
