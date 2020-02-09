using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private float aggro_x = 5;
    [SerializeField]
    private float aggro_y;

    private float timer_shoot = 0f;
    private float timer_phase = 0f;

    [SerializeField]
    private float firespeed = 0f;


    [SerializeField]
    private float time_phase1 = 5f;
    [SerializeField]
    private float time_phase2 = 7f;
    [SerializeField]
    private float time_phase3 = 4f;

    [SerializeField]
    private float firespeed_phase1 = 0.5f;
    [SerializeField]
    private float firespeed_phase2 = 1f;
    [SerializeField]
    private float firespeed_phase3 = 2f;

    private int current_phase; // 1 et 3 : little ammo ; 2 et 4 : shockwave ; 5 : big ammo

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

    private Animator animator;
    private bool awaken = false;



    [SerializeField]
    public int health = 1000;
    private int healthMax;

    [SerializeField]
    public GameObject healthBarCurrent;
    private RectTransform healthBarCurrent_img;


    [SerializeField]
    public GameObject healthBarEmpty;
    private float healthBarMax;

    [SerializeField]
    public GameObject healthText;
    private Text healthTextText;





    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aggro_y = transform.localScale.y;
        current_phase = 1;

        //Healthbar initialisation
        this.healthBarCurrent_img = healthBarCurrent.GetComponent<RectTransform>();
        this.healthMax = health;
        healthBarMax = healthBarEmpty.GetComponent<RectTransform>().rect.width;
        healthTextText = healthText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthBar();

        timer_shoot += Time.deltaTime;
        timer_phase += Time.deltaTime;

        switch(current_phase)
        {
            case 1:
                firespeed = firespeed_phase1;
                break;
            case 2:
                firespeed = firespeed_phase2;
                break;
            case 3:
                firespeed = firespeed_phase1;
                break;
            case 4:
                firespeed = firespeed_phase2;
                break;
            case 5:
                firespeed = firespeed_phase3;
                break;
        }
        detectPlayer();
        switch (current_phase)
        {
            case 1:
                if (timer_phase > time_phase1) changePhase();
                break;
            case 2:
                if (timer_phase > time_phase2) changePhase();
                break;
            case 3:
                if (timer_phase > time_phase1) changePhase();
                break;
            case 4:
                if (timer_phase > time_phase2) changePhase();
                break;
            case 5:
                if (timer_phase > time_phase3) changePhase();
                break;
        }
    }

    private void changePhase()
    {
        current_phase++;
        if(current_phase == 6)
        {
            current_phase = 1;
        }
        timer_phase = 0f;
    }

   private void detectPlayer()
    {
        if (timer_shoot > firespeed)
        {
            Vector2 basePoint = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2);
            RaycastHit2D hit = Physics2D.BoxCast(basePoint, new Vector2(aggro_x * 3, aggro_y * 3), 0, new Vector2(-1, 1));

            // If it hits something...
            if (hit.collider != null && hit.collider.name == player.name)
            {
                if (awaken)
                {
                    shoot(hit.collider.gameObject);
                }
            }
            timer_shoot = 0f;
        }
        
    }

    private void shoot(GameObject player)
    {
        switch(current_phase)
        {
            case 1:
                shootLittle(player);
                break;
            case 2:
                shootWave();
                break;
            case 3:
                shootLittle(player);
                break;
            case 4:
                shootWave();
                break;
            case 5:
                shootBig(player);
                break;
        }

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

    private void shootWave()
    {
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




    // Fonctions relatives aux dégâts subis par le boss


    private void updateHealthBar()
    {
        float newWidth = health * healthBarMax / healthMax; // Produit en croix

        Rect rect = healthBarCurrent_img.rect;
        healthBarCurrent_img.sizeDelta = new Vector2(newWidth, healthBarCurrent_img.rect.height);

        healthTextText.text = health + "/" + healthMax;
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

        health = Mathf.Max(0, health - damage);

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
        Destroy(gameObject, 0.8f);
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
