using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private ParticleSystem particle;

    private GameObject firstPlayer;
    private bool reached;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !reached)
        {
            if (firstPlayer != null)
            {
                if (collision.gameObject != firstPlayer)
                {
                    reached = true;
                    particle.Play();
                    GetComponent<Animator>().SetTrigger("Checkpoint");
                    GameManager.instance.NewCheckpoint(transform.position - Vector3.up * 0.5f - Vector3.right * 0.5f);
                }
            }
            else
            {
                firstPlayer = collision.gameObject;
            }
        }
    }
}
