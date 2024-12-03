using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    private int bananas = 0;
    [SerializeField]private Text bananaText;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Banana"))
            {
                Destroy(collision.gameObject);
                bananas++;
                bananaText.text = "bananas:" + bananas;
            }
        }
    }
}
