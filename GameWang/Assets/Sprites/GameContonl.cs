using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContonl : MonoBehaviour
{
    public Animator animator;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsGround", player.IsGround);
        animator.SetBool("IsOnWall", player.IsOnWall);

        animator.SetFloat("HorizontalSpeed",Mathf.Abs(player.Horizontal));
        animator.SetFloat("VerticalSpeed", player.rigidbody2.velocity.y);
    }
}
