using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConcotil : MonoBehaviour
{
    public Transform tartgetPlayer;
    public float tartgetSpeed = 0.2f;

    public float minLimit = 0;
    public float maxLimit = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tartgetPlayer==null)
        {
            return;
        }

        Vector3 tertgerPos = new Vector3(tartgetPlayer.position.x, transform.position.y, transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, tertgerPos, tartgetSpeed);

        if (this.transform.position.x < minLimit)
        {
            this.transform.position = new Vector3(minLimit,this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > maxLimit)
        {
            this.transform.position = new Vector3(maxLimit, this.transform.position.y, this.transform.position.z);
        }
    }
}
