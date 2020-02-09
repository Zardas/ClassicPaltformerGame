using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Make_BossHealBar_appear : MonoBehaviour
{

    [SerializeField]
    private Image boss_healthbarCurrent;
    [SerializeField]
    private Image boss_healthbarEmpty;
    [SerializeField]
    private Text boss_healthbarText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // The boss healthbars appears as soon as the player enter the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            boss_healthbarCurrent.enabled = true;
            boss_healthbarEmpty.enabled = true;
            boss_healthbarText.enabled = true;
        }
    }

    // The boss healthbars disappears as soon as the player enter the collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss_healthbarCurrent.enabled = false;
            boss_healthbarEmpty.enabled = false;
            boss_healthbarText.enabled = false;
        }
    }
}
