using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MUshroomTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed;//速度
    public Animator anim;
    private float timer;//计时器
    private int i = 1;

    public SpriteRenderer SR;
    public float startWaitTime = 2.0f;
    public Transform[] partrolPoints;//巡逻点



    // Start is called before the first frame update
    void Start()
    {
        timer = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, partrolPoints[i].transform.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, partrolPoints[i].transform.position) < 0.1f)
        {
            if(timer <=0)
            {
                if (partrolPoints[i] != partrolPoints[partrolPoints.Length -1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                timer = startWaitTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        checkMove();
    }
    private void checkMove()
    {
        if (transform.position.x > partrolPoints[i].position.x)
        {
            SR.flipX = false;
            anim.SetBool("state", true);
        }
        else if (transform.position.x < partrolPoints[i].position.x)
        {
            SR.flipX = true;
            anim.SetBool("state", true);
        }
        else
        {
            anim.SetBool("state", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("killpoint"))
        {
            anim.SetTrigger("die");
        }
    }
}
