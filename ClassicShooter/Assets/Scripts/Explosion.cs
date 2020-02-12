using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    public int damage = 50;

    [SerializeField]
    private float lifespan = 2.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.animator.Play("orb_death");
    }

    // Update is called once per frame
    void Update()
    {
            die();
    }

    private void die()
    {
        
        Destroy(gameObject, lifespan);
    }

}
