using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platfrom : MonoBehaviour
{
    PlatformEffector2D pe2d;
    // Start is called before the first frame update
    void Start()
    {
        pe2d = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            pe2d.rotationalOffset = 180f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pe2d.rotationalOffset = 0f;
        }

    }
}
