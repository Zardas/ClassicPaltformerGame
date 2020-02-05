using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckLeft;

    [SerializeField]
    Transform groundCheckRight;

    [SerializeField]
    float jumpSpeed = 8;

    [SerializeField]
    float runSpeed = 6;

    [SerializeField]
    GameObject gun;

    bool isGrounded;
    bool faceRight;


    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {

        //Check if the player is on a floor (and so if he can jump)
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }



        if (Input.GetKey(KeyCode.LeftArrow)) // LEFT
        {

            rigidBody2D.velocity = new Vector2(-runSpeed, rigidBody2D.velocity.y);

            if (this.faceRight)
            {
                this.flip();
            }

            if (this.isGrounded)
            {
                //animator.Play("Player_run");
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // DROITE
        {
            rigidBody2D.velocity = new Vector2(runSpeed, rigidBody2D.velocity.y);

            if (!this.faceRight)
            {
                this.flip();
            }

            if (this.isGrounded)
            {
                //animator.Play("Player_run");
            }
        }
        else
        {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);

            if (isGrounded)
            {
                //animator.Play("Player_idle");
            }
        }



        // SAUT
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
            //animator.Play("Player_jump");
        }
    }


    private void flip()
    {
        this.faceRight = !this.faceRight;
        transform.Rotate(0, 180f, 0);
    }
}
