using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Rewired;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
   // private Player player;
    public bool useController;


    public Animator animator;
    public Rigidbody2D rb;

    Vector3 movement;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
    }

    private void ProcessInputs()
    {
       movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }

    private void Move()
    {
        transform.position = transform.position + movement * Time.deltaTime;
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }
}
