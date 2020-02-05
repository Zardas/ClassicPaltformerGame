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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    private void shoot()
    {
        Debug.Log("Shoot");
        GameObject ammoPrefab = Instantiate(ammo, firepoint.transform.position, firepoint.transform.rotation);
        ammoPrefab.transform.SetParent(prefabRepository.transform);

    }
}
