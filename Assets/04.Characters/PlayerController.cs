using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    [SerializeField] private float moveSpeed;
    private bool jump = false;
    [SerializeField] private float jumpForce;
    private bool isOnGround = false;
    private bool canDoubleJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetMovement();
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
        if (jump)
        {
            rb.velocity = Vector2.up * jumpForce;
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isOnGround = true;
            canDoubleJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
            isOnGround = false;
    }

    private void GetMovement()
    {
        velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                jump = true;
            }
            else if (canDoubleJump)
            {
                jump = true;
                canDoubleJump = false;
            }
        }
    }
}
