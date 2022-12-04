using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 20f;
    public float sprintMult = 1f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    float block = 1f;

    float dashSpeed = 40f;

    bool dashed = false;
    bool dashCounterTriggered = false;

    Rigidbody2D rb;

    Animator anim;

    float combinedSpeed = 0f;


    public float dashCooldown = 4;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;

        combinedSpeed = (speed * block);

        if (Input.GetButton("Fire1"))
        {
           if(dashed == false)
            {
                combinedSpeed *= dashSpeed;
                dashed = true;
                dashCounterTriggered = true;
            }

            print("Dashed!!!!");

        }

        if(dashCounterTriggered == true)
        {

            dashCooldown -= Time.deltaTime;

            if(dashCooldown <= 0)
            {
                dashCooldown = 4;
                dashed = false;
                dashCounterTriggered = false;
            }

        }
 

    }

    void animationUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            anim.SetFloat("Blend", 0);
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            anim.SetFloat("Blend", 0.66f);
        }

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("Blend", 1);
        }

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            anim.SetFloat("Blend", 0.33f);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * combinedSpeed * Time.deltaTime, verticalMove * combinedSpeed * Time.deltaTime);
       
    }
}
