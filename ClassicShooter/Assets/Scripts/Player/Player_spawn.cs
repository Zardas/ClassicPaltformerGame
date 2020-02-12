using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_spawn : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject playerObject;

    // Start is called before the first frame update
    public void spawnPlayer(GameObject player)
    {
        player.GetComponent<Player_takeDamage>().ressucite();
        player.GetComponent<Player_movement>().ressucite();
        
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;

    }
}
