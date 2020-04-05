using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    public bool useController;

    private bool attacking;


    public Animator animator;
    public GameObject crossHair;

    public GameObject arrowPrefab;

    public Rigidbody2D rb;

    Vector3 movement;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        ProcessInputs();
        Animate();
        Move();
        AimAndShoot();
        Run();
        Attack();
        

        




    }

    private void ProcessInputs()
    {
        if (useController)
        {
            movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);
            aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            isAiming = Input.GetButton("ShootC");
            endOfAiming = Input.GetButtonUp("ShootC");

        }
        else
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            FindObjectOfType<AudioManager>().Play("Run");

            aim = aim + mouseMovement;
            if (aim.magnitude > 1.0f)
            {
                aim.Normalize();
            }
            isAiming = Input.GetButton("ShootM");
            endOfAiming = Input.GetButtonUp("ShootM");
            endOfAiming = Input.GetButtonUp("ShootM");
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
        
        //animator.SetFloat("AimHorizontal", aim.x);
        //animator.SetFloat("AimVertical", aim.y);
        //animator.SetFloat("AimMagnitude", aim.magnitude);
       // animator.SetBool("Aim", isAiming);


    }

    private void Move()
    {
        //transform.position = transform.position + movement * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    private void Attack()
    {
        if (useController)
        {
            if (Input.GetButton("AttackC"))
            {
                Debug.Log("ATTACK Controller");
                FindObjectOfType<AudioManager>().Play("Attack");
            }
        } else
        {
            if (Input.GetButton("AttackM"))
            {
                Debug.Log("ATTACK Mouse");
                FindObjectOfType<AudioManager>().Play("Attack");
            }
        }
    }

    private void Shoot()
    {
        if (useController)
        {
            if (Input.GetButtonDown("ShootC"))
            {
                Debug.Log("Shoot Controller");
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 0.0f);
            }
        } else
        {
            if (Input.GetButtonDown("ShootM"))
            {
                Debug.Log("Shoot Mouse");
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 0.0f);
            }
        }
    }

    private void Run()
    {
        if (Input.GetButton("Run"))
        {
            Debug.Log("RUN");
        }
    }

    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if (aim.magnitude > 0.0f)
        {
            
           
            crossHair.transform.localPosition = aim * 0.4f;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming)
            {
                Debug.Log("FIRE!");
                FindObjectOfType<AudioManager>().Play("Shoot");
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, 2.0f);
            }

        } else
        {
            crossHair.SetActive(false);
        }

    }
}