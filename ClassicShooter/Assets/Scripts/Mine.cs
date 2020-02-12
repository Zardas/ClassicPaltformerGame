using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    public int amount = 75;

    [SerializeField]
    private GameObject explosion;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void die()
    {
        //GameObject explosion_prefab = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
