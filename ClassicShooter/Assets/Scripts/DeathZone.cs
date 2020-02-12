using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    private GameHandler gameHandler;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameHandler.GetComponent<Player_spawn>().spawnPlayer(collision.gameObject);
    }
}
