using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    public int damage = 50;

    private float timer = 0.0f;
    [SerializeField]
    private float lifespan = 2.0f;

    private Animator animator;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.animator.Play("Explosion_3");
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifespan)
        {
            die();
        }
    }

    private void die()
    {
        
        Destroy(gameObject);
    }

}
