using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilleSpawner : MonoBehaviour
{
    public static AnvilleSpawner instance;

    public GameObject anvillePrefab;
    public List<GameObject> spawnedAnvilles = new List<GameObject>();

    public float spawnInterval;

    private bool activated;


    private void Awake()
    {
        instance = this;
        GameManager.OnReset += PlayerDied;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            StartCoroutine(SpawnAnville());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && activated)
        {
            activated = false;
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnAnville()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-25, 25), Random.Range(-5, 5));
        GameObject anville = Instantiate(anvillePrefab, (Vector2)transform.position + spawnPos, Quaternion.identity);
        anville.transform.parent = transform;
        spawnedAnvilles.Add(anville);

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(SpawnAnville());
    }


    public void PlayerDied()
    {
        foreach (GameObject anville in spawnedAnvilles)
        {
            Destroy(anville);
        }
        spawnedAnvilles.Clear();
    }
}
