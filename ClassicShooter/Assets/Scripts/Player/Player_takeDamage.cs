using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_takeDamage : MonoBehaviour
{

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
        this.healthBarCurrent_img = healthBarCurrent.GetComponent<RectTransform>();
        this.healthMax = health;

        healthBarMax = healthBarEmpty.GetComponent<RectTransform>().rect.width;

        healthTextText = healthText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float newWidth = health * healthBarMax / healthMax; // Produit en croix

        Rect rect = healthBarCurrent_img.rect;
        healthBarCurrent_img.sizeDelta = new Vector2(newWidth, healthBarCurrent_img.rect.height);

        healthTextText.text = health + "/" + healthMax;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Le joueur se fait toucher par " + collision.name);


        // Ammo
        Ammo ammo = collision.GetComponent<Ammo>();
        if (ammo != null)
        {
            TakeDamage(ammo.damage);
        }

    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }
}
