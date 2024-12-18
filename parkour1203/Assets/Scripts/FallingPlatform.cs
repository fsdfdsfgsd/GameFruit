using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1.5f;//��׹�ӳ�ʱ��
    [SerializeField] private float destroyDelay = 1.0f;//�����ӳ�ʱ��

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            StartCoroutine(Fall());//��Э�̵��÷���
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);//�ȴ�ʱ��
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject,destroyDelay);
    }
}
