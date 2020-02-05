using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_aimWeapon : MonoBehaviour
{
    //private Transform aimTransform;

    private Animator animator;

    [SerializeField]
    GameObject aim;

    [SerializeField]
    GameObject gun;

    private bool alreadyRotated;

    private void Awake()
    {
        this.alreadyRotated = false;
        //this.aimTransform = transform.Find("Aim");
        //this.animator = gun.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gunRotation();
        this.gunShooting();
    }



    private void gunRotation()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        

        if(angle > 90 && angle < 280) //LEFT
        {
            aim.transform.eulerAngles = new Vector3(0, 0, angle + 20);
            if (!alreadyRotated)
            {
                gun.transform.Rotate(180f, 0, 0);
                alreadyRotated = !alreadyRotated;
            } 
        }
        if(!(angle > 90 && angle < 280)) //RIGHT
        {
            aim.transform.eulerAngles = new Vector3(0, 0, angle - 20);
            if(alreadyRotated)
            {
                gun.transform.Rotate(180f, 0, 0);
                alreadyRotated = !alreadyRotated;
            }
        }

        //Debug.Log("Rotation actuelle : " + aim.transform.eulerAngles);

    }


    private void gunShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Click gauche
        {
            //animator.SetTrigger("Shoot");
        }
    }


    // From codeMonkey : https://unitycodemonkey.com/video.php?v=fuGQFdhSPg4
    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
