using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatfrom : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 2f;
    private int pointNum;
    private float withTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[pointNum].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[pointNum].transform.position) < 0.1f)
        {
            if (withTime < 0)
            {
                if(pointNum == 0)
                {
                    pointNum = 1;
                }
                else
                {
                    pointNum = 0;
                }
            }
            else
            {
                withTime -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
