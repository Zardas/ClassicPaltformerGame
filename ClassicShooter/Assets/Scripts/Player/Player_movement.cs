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

    private Animator animator;

    [SerializeField]
    public float topLimit;
    [SerializeField]
    public float leftLimit;
    [SerializeField]
    public float rightLimit;
    [SerializeField]
    public float bottomLimit;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();

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
            if (rigidBody2D.position.x > leftLimit)
            {
                rigidBody2D.velocity = new Vector2(-runSpeed, rigidBody2D.velocity.y);
            }

            if (this.faceRight)
            {
                this.flip();
            }

            if (this.isGrounded)
            {
                this.animator.Play("Hero_run");
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // DROITE
        {
            if (rigidBody2D.position.x < rightLimit)
            {
                rigidBody2D.velocity = new Vector2(runSpeed, rigidBody2D.velocity.y);
            }

            if (!this.faceRight)
            {
                this.flip();
            }

            if (this.isGrounded)
            {
                this.animator.Play("Hero_run");
            }
        }
        else
        {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);

            if (isGrounded)
            {
                //this.animator.Play("Hero_idle"); //Messe up with the shoot
            }
        }



        // SAUT
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            if (rigidBody2D.position.y < topLimit)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
            }
            this.animator.Play("Hero_jump");
        }
    }


    private void flip()
    {
        this.faceRight = !this.faceRight;
        transform.Rotate(0, 180f, 0);
    }




    //Gizmo = élément graphiques dans l'éditeur pour les devs
    private void OnDrawGizmos()
    {
        //Carré pour la caméra
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(rightLimit, topLimit));

        Gizmos.DrawIcon(new Vector2(leftLimit + 1, topLimit - 1), "Player_icon.png", true);
    }
}
