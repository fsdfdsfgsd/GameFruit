using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceView : MonoBehaviour
{
    private Transform currentPlayer;
    public float minDistance = 10;

    public bool IsSleep = true;
    public float maxDistanceDelta = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = FindObjectOfType<Player>().gameObject.transform;
        IsSleep = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer == null)
        {
            return;
        }
        

        float distance = Vector3.Distance(transform.position, currentPlayer.position);
        if (distance <= minDistance)
        {
            IsSleep = false;

            if(transform.position.x > currentPlayer.position.x)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
            if(transform.position.x < currentPlayer.position.x)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }


            this.transform.position = Vector3.MoveTowards(this.transform.position,currentPlayer.position,maxDistanceDelta*Time.deltaTime);
        }
    }
}
