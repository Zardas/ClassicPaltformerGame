using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalPanelAppear : MonoBehaviour
{

    [SerializeField]
    private Image panel;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panel.enabled = true;
            text.enabled = true;
            button.image.enabled = true;
            button.GetComponentInChildren<Text>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panel.enabled = false;
            text.enabled = false;
            button.image.enabled = false;
            button.GetComponentInChildren<Text>().enabled = false;
        }
    }
}
