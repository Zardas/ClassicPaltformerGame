using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Level : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
