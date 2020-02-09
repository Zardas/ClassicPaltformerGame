using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour
{

    [SerializeField]
    public GameObject firepoint;

    [SerializeField]
    public GameObject ammo;

    [SerializeField]
    public GameObject prefabRepository;

    [SerializeField]
    private float firespeed = 1;
    private float timer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.timer = 0;
        this.animator = transform.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;

        if(Input.GetButtonDown("Fire1") && timer > firespeed / 2)
        {
            shoot();
            timer = 0;
        }
    }

    private void shoot()
    {
        Debug.Log("Shoot");
        //this.animator.Play("Hero_attack");
        this.animator.SetTrigger("Attack");
        GameObject ammoPrefab = Instantiate(ammo, firepoint.transform.position, firepoint.transform.rotation);
        ammoPrefab.transform.SetParent(prefabRepository.transform);

    }



    
}
