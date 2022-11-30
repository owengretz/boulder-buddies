using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleButton : MonoBehaviour
{
    public Sprite buttonUpSprite;
    public Sprite buttonDownSprite;
    public float speed;


    public bool twoWalls;
    [ConditionalHide("twoWalls", true)]
    public Transform topWall;
    [ConditionalHide("twoWalls", true)]
    public Transform bottomWall;


    private bool opened;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        rend.sprite = buttonDownSprite;

        if (twoWalls)
        {
            StartCoroutine(OpenWalls());
            opened = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (!opened) rend.sprite = buttonUpSprite;
    }

    private IEnumerator OpenWalls()
    {
        float timer = 10f;
        while (timer > 0f)
        {
            topWall.position += Vector3.up * speed * Time.deltaTime;
            bottomWall.position -= Vector3.up * speed * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }
    }


}
