using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private float aggro_x = 5;
    [SerializeField]
    private float aggro_y;

    private float timer = 0f;
    [SerializeField]
    private float phase1_speed = 0.5f;
    [SerializeField]
    private float phase2_speed;
    [SerializeField]
    private float phase3_speed;

    [SerializeField]
    private Transform firepoint1;
    [SerializeField]
    private Transform firepoint2;

    [SerializeField]
    private GameObject ammoLittle;
    [SerializeField]
    private GameObject ammoBig;
    [SerializeField]
    private GameObject ammoWave;

    [SerializeField]
    public GameObject prefabRepository;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    public int health = 100;

    private Animator animator;
    private bool awaken = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Flag a");
        animator = GetComponent<Animator>();
        aggro_y = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        detectPlayer();
    }


   private void detectPlayer()
    {
        if (timer > phase1_speed)
        {
            //Debug.Log("Flag c");
            //Vector2 basePoint = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2);
            Vector2 basePoint = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2);
            RaycastHit2D hit = Physics2D.BoxCast(basePoint, new Vector2(aggro_x * 3, aggro_y * 3), 0, new Vector2(-1, 1));

            // If it hits something...
            if (hit.collider != null && hit.collider.name == player.name)
            {
                //FirepointRotation(hit.collider.gameObject);
                //Debug.Log("Le boss shoot sur " + hit.collider.name);
                if (awaken)
                {
                    shoot(hit.collider.gameObject);
                }
            }
            timer = 0f;
        }
        
    }

    private void shoot(GameObject player)
    {
        //Debug.Log("Le boss shoot !");
        //shootLittle(player);
        shootWave(player);
    }



    private void shootLittle(GameObject player)
    {
        FirepointRotation(player);
        GameObject ammoPrefab = Instantiate(ammoLittle, firepoint1.transform.position, firepoint1.transform.rotation);
        ammoPrefab.transform.SetParent(prefabRepository.transform);
    }

    private void shootBig(GameObject player)
    {
        FirepointRotation(player);
        GameObject ammoPrefab = Instantiate(ammoBig, firepoint1.transform.position, firepoint1.transform.rotation);
        ammoPrefab.transform.SetParent(prefabRepository.transform);
    }

    private void shootWave(GameObject player)
    {
        FirepointRotation(player);
        GameObject ammoPrefab = Instantiate(ammoWave, firepoint2.transform.position, firepoint2.transform.rotation);
        ammoPrefab.transform.SetParent(prefabRepository.transform);
    }










    private void FirepointRotation(GameObject player)
    {
        Vector3 playerPosition = player.transform.position;

        Vector3 aimDirection = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        firepoint1.eulerAngles = new Vector3(0, 0, angle+20);

        Debug.Log("Rotation actuelle : " + firepoint1.transform.eulerAngles);

    }








    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Boss touchée avec " + collision.name);


        // Ammo
        Ammo ammo = collision.GetComponent<Ammo>();
        if (ammo != null)
        {
            TakeDamage(ammo.damage);
        }


        //Explosion
        Explosion explosion = collision.GetComponent<Explosion>();
        if (explosion != null)
        {
            TakeDamage(explosion.damage);
        }

    }


    public void TakeDamage(int damage)
    {
        awaken = true;

        health -= damage;

        //Barre de vie
        //healthBar.transform.localScale = new Vector3((float)(health * 1 / healthMax), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        animator.Play("demon_death");
        Destroy(gameObject, 1.0f);
    }


    private void OnDrawGizmos()
    {
        Vector2 basePoint = new Vector2(transform.position.x, transform.position.y);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(basePoint, new Vector2(basePoint.x - aggro_x, basePoint.y));
        Gizmos.DrawLine(new Vector2(basePoint.x - aggro_x, basePoint.y), new Vector2(basePoint.x-aggro_x, basePoint.y+aggro_y));
        Gizmos.DrawLine(new Vector2(basePoint.x - aggro_x, basePoint.y + aggro_y), new Vector2(basePoint.x, basePoint.y+aggro_y));
    }
}
