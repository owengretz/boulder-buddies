using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : MonoBehaviour
{
    public static Juggle instance;

    [HideInInspector] public Collider2D col;
    [HideInInspector] public int player = -1;

    private void Awake()
    {
        instance = this;
        col = GetComponent<Collider2D>();
        GameManager.OnReset += PlayerDied;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int playerNumber = collision.gameObject.GetComponent<PlayerMovement>().playerNumber;

            if (player == -1)
            {
                player = playerNumber;
            }
            else if (playerNumber != player)
            {
                GameManager.instance.PlayerDied();
            }
        }
    }

    // make trigger exit instead
    public void PlayerDied()
    {
        player = -1;
    }
}
