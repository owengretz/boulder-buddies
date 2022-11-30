using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform wall;
    public Sprite buttonUpSprite;
    public Sprite buttonDownSprite;
    public int number;
    public int dir;
    public float moveSpeed;

    public Button otherButton;
    [HideInInspector] public bool pressed;
    private bool opened;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressed = true;
            rend.sprite = buttonDownSprite;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressed = false;
            rend.sprite = buttonUpSprite;
        }
    }

    private void Update()
    {
        if (pressed && otherButton.pressed && !opened)
        {
            StartCoroutine(RaiseWall());
            opened = true;
        }
    }

    private IEnumerator RaiseWall()
    {
        float timer = 5f;
        while (timer > 0f)
        {
            wall.position = new Vector2(wall.position.x, wall.position.y + moveSpeed * Time.deltaTime * dir);

            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
