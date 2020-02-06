using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin_script : MonoBehaviour
{

    [SerializeField]
    public float health = 100;
    private float healthMax;

    [SerializeField]
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthMax = health;
        healthBar.transform.localScale = new Vector3(health * 1 / healthMax, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
