﻿using System.Collections;
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
        move();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);


        // Ammo
        Ammo ammo = collision.GetComponent<Ammo>();
        if (ammo != null)
        {
            TakeDamage(ammo.damage);
        }

        
        /*Plateform plateform = collision.GetComponent<Plateform>();
        if (plateform != null)
        {
            TakeDamage(ammo.damage);
        }*/

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
    }
}