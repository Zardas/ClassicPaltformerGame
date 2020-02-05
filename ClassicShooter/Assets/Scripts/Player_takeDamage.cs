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
        health = Mathf.Max(0, health-1);
        Debug.Log("Health : " + health);
        float newWidth = health * healthBarMax / healthMax; // Produit en croix

        Debug.Log("Width : " + healthBarCurrent_img.rect.width);
        Rect rect = healthBarCurrent_img.rect;
        healthBarCurrent_img.sizeDelta = new Vector2(newWidth, healthBarCurrent_img.rect.height);

        healthTextText.text = health + "/" + healthMax;
    }
}
