using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 20f;
    public float sprintMult = 1f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, verticalMove * speed * Time.deltaTime);
        
    }
}
