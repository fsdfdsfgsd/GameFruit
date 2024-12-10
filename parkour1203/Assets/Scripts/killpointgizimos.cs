using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killpointgizimos : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(transform.position,new Vector3(0.3f, 0.5f, 0f));
    }
}
