using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    GameObject player;


    [SerializeField]
    public float topLimit;
    [SerializeField]
    public float leftLimit;
    [SerializeField]
    public float rightLimit;
    [SerializeField]
    public float bottomLimit;

    [SerializeField]
    private GameObject ammoDirectory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /* Si on veux que la caméra place le perso sur la gauche
        Vector3 startPosition = transform.position;


        Vector3 endPosition = player.transform.position;

        endPosition.x += posOfset.x;
        endPosition.y += posOfset.y;
        endPosition.z += -10;

        transform.position = Vector3.Lerp(startPosition, endPosition, timeOfset * Time.deltaTime);*/

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10); //Position of the camera

        //Adjust to camera boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
            );


        destroy_outsideBox_ammo(); //We destroy the ammo that are outside the camera
    }


    private void destroy_outsideBox_ammo()
    {
        for(int i = 0; i < ammoDirectory.transform.childCount; i++)
        {
            Vector3 ammoPosition = ammoDirectory.transform.GetChild(i).position;

           if (ammoPosition.x < leftLimit ||
               ammoPosition.x > rightLimit ||
               ammoPosition.y < bottomLimit ||
               ammoPosition.y > topLimit)
            {
                Debug.Log("Destruction validée");
                Destroy(ammoDirectory.transform.GetChild(i).gameObject);
            }
        }
        Debug.Log("Test destruction ammo");
        
    }


    //Gizmo = élément graphiques dans l'éditeur pour les devs
    private void OnDrawGizmos()
    {
        //Carré pour la caméra
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(rightLimit, topLimit));

        Gizmos.DrawIcon(new Vector2(leftLimit + 1, topLimit - 1), "Camera_icon.png", true);
    }
}
