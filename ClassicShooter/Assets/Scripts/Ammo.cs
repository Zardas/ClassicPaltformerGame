using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [SerializeField]
    public float speed = 20f;

    [SerializeField]
    public int damage = 25;

    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        Mannequin_script mannequin = collision.GetComponent<Mannequin_script>();
        if (mannequin != null)
        {
            mannequin.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
