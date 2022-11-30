using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PlayerDied();
        }
    }
}
