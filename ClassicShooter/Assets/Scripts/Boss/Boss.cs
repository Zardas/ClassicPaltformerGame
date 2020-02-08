using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private float aggro_x = 5;
    [SerializeField]
    private float aggro_y;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float Firepoint1_speed;
    [SerializeField]
    private float Firepoint2_speed;
    [SerializeField]
    private float Firepoint3_speed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Flag a");
        aggro_y = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }


   private void detectPlayer()
    {
        //Debug.Log("Flag c");
        Vector2 basePoint = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2);
        RaycastHit2D hit = Physics2D.BoxCast(basePoint, new Vector2(aggro_x, aggro_y), 0, new Vector2(-1,1));

        // If it hits something...
        //Debug.Log("Boss : ");
        //Debug.Log("Boss : " + hit.collider.name);
        if (hit.collider != null && hit.collider.name == player.name)
        {
            Debug.Log("Le boss shoot sur " + hit.collider.name);
            shoot();
        }
    }

    private void shoot()
    {
        //Debug.Log("Le boss shoot !");
    }

    private void OnDrawGizmos()
    {
        Vector2 basePoint = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(basePoint, new Vector2(basePoint.x - aggro_x, basePoint.y));
        Gizmos.DrawLine(new Vector2(basePoint.x - aggro_x, basePoint.y), new Vector2(basePoint.x-aggro_x, basePoint.y+aggro_y));
        Gizmos.DrawLine(new Vector2(basePoint.x - aggro_x, basePoint.y + aggro_y), new Vector2(basePoint.x, basePoint.y+aggro_y));
    }
}
