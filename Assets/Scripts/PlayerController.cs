using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    public bool useController;

    public Animator animator;

    public Rigidbody2D rb;

    Vector3 movement;
   

    

    void Update()
    {
        ProcessInputs();
        Animate();
        Move();

        if (Input.GetButton("Fire"))
        {
            Debug.Log("FIRE!");
        }

        

        
    }

    private void ProcessInputs()
    {
        if (useController)
        {
            movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);
        } else
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        }

        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    private void Move()
    {
        //transform.position = transform.position + movement * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, movement.y);
    }
}
