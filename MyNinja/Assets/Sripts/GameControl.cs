using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
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
        animator.SetBool("IsGround",player.isGround);
        animator.SetBool("IsOnWall", player.isWall);
        animator.SetFloat("VerticalSpeed", player.Rigidbody2D.velocity.y);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(player.Rigidbody2D.velocity.x));
    }
}
