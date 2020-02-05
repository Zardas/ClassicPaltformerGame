﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin_script : MonoBehaviour
{

    [SerializeField]
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            die(); 
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }
}
