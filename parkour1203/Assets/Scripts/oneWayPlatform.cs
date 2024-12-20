using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneWayPlatform : MonoBehaviour
{
    private GameObject currentPlatform;  //��ǰƽ̨
    [SerializeField] private CapsuleCollider2D playerCollider;  //player����ײ��

    private float reocveryTime = 0.3f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            if(currentPlatform != null)
            {
                StartCoroutine(DisableCollsion());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("oneWayPlatform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("oneWayPlatform"))
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollsion()
    {
        BoxCollider2D paltfromCollider = currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, paltfromCollider);

        yield return new WaitForSeconds(reocveryTime);

        Physics2D.IgnoreCollision(playerCollider, paltfromCollider,false);

    }

}
