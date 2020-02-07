using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    public int health = 150;
    private float healthMax;

    [SerializeField]
    public GameObject healthBar;


    [SerializeField]
    public float leftWalkLimit;
    [SerializeField]
    public float rightWalkLimit;

    [SerializeField]
    public bool beginFacingLeft;

    private int faceRight = 1;

    [SerializeField]
    public int speed;

    private Rigidbody2D rigidBody2D;

    [SerializeField]
    public GameObject collisionCheck;

    [SerializeField]
    public GameObject player;


    [SerializeField]
    public GameObject firepoint;

    [SerializeField]
    public GameObject ammo;

    [SerializeField]
    public GameObject prefabRepository;


    private float timer = 0.0f;
    [SerializeField]
    public float fireSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        healthMax = health;
        healthBar.transform.localScale = new Vector3(health * 1 / healthMax, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        this.rigidBody2D = GetComponent<Rigidbody2D>();

        if(beginFacingLeft)
        {
            flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(leftWalkLimit != 0 && rightWalkLimit != 0)
        {
            move();
        }
    }


    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        PlayerDetection();
    }


    private void move()
    {
        rigidBody2D.velocity = new Vector2(speed*faceRight, rigidBody2D.velocity.y);

        //On vérifie si on est arrivé au bout de la ligne
        if(collisionCheck.transform.position.x > rightWalkLimit || //Si on est arrivé au bout de la ligne à droite
            collisionCheck.transform.position.x < leftWalkLimit || //Si on est arrivé au bout de la ligne à gauche
            (Physics2D.Linecast(transform.position, collisionCheck.transform.position, 1 << LayerMask.NameToLayer("Obstacle"))))
        {
            flip();
        }
    }

    private void flip()
    {
        this.faceRight *= -1;
        transform.Rotate(0, 180f, 0);
    }


    private void PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x + (transform.localScale.x/4)*faceRight, transform.position.y + transform.localScale.y/4),
            new Vector2(transform.position.x + (transform.localScale.x/4 + 10)*faceRight, transform.position.y + transform.localScale.y/4)
            );

        // If it hits something...
        if (hit.collider != null)
        {
            if (hit.collider.name == player.name && timer > fireSpeed)
            {
                shoot();
                timer -= fireSpeed;
            }
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            //float distance = Mathf.Abs(hit.point.y - transform.position.y);
            //float heightError = floatHeight - distance;

            // The force is proportional to the height error, but we remove a part of it
            // according to the object's speed.
            //float force = liftForce * heightError - rb2D.velocity.y * damping;

            // Apply the force to the rigidbody.
            //rb2D.AddForce(Vector3.up * force);
        }
    }

    private void shoot()
    {
        //Debug.Log("Shoot");
        GameObject ammoPrefab = Instantiate(ammo, firepoint.transform.position, firepoint.transform.rotation);
        //ammoPrefab.transform.SetParent(prefabRepository.transform);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);


        // Ammo
        Ammo ammo = collision.GetComponent<Ammo>();
        if (ammo != null)
        {
            TakeDamage(ammo.damage);
        }


        //Explosion
        Explosion explosion = collision.GetComponent<Explosion>();
        if (explosion != null)
        {
            TakeDamage(explosion.damage);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Obstacle
        //Debug.Log(collision.gameObject.transform.parent.name);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        //Barre de vie
        healthBar.transform.localScale = new Vector3((float)(health * 1 / healthMax), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }


    //Gizmo = élément graphiques dans l'éditeur pour les devs
    private void OnDrawGizmos()
    {
        //Ligne pour réprésenter jusqu'où le mob peut aller
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftWalkLimit, transform.position.y + transform.localScale.y/4), new Vector2(rightWalkLimit, transform.position.y + transform.localScale.y/4));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(transform.position.x + (transform.localScale.x/4)*faceRight, transform.position.y + 3*(transform.localScale.y / 8)),
            new Vector2(transform.position.x + (transform.localScale.x/4 + 10)* faceRight, transform.position.y + 3*(transform.localScale.y / 8))); //Ligne de vue
    }
}
