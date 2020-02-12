using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelAppear : MonoBehaviour
{
    [SerializeField]
    private Image panel;
    [SerializeField]
    private Text text;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panel.enabled = true;
            text.enabled = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panel.enabled = false;
            text.enabled = false;
        }
    }
}
