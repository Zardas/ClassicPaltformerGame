using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrel : MonoBehaviour
{
    [SerializeField]
    public int health = 1;

    [SerializeField]
    private GameObject explosion;

    private Rigidbody2D rigidBody;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Ammo
        Ammo ammo = collision.GetComponent<Ammo>();
        if (ammo != null)
        {
            TakeDamage(ammo.damage);
        }

    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        GameObject explosion_prefab = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject, 0.3f);
    }

}
