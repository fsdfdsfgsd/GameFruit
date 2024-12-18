using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1.5f;//下坠延迟时间
    [SerializeField] private float destroyDelay = 1.0f;//销毁延迟时间

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            StartCoroutine(Fall());//用协程调用方法
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);//等待时间
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject,destroyDelay);
    }
}
