using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    public GameObject deathParticlePrefab;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    private bool canJump;
    public float jumpVelocity;

    private Rigidbody2D rb;
    private Transform trans;

    private string jumpButtonName;
    private string horInputName;
    private float horInputValue;

    public float moveForce;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        trans = GetComponentInChildren<Transform>();
        horInputName = "Horizontal" + playerNumber;
        jumpButtonName = "Jump" + playerNumber;
    }

    private void Update()
    {
        if (playerNumber == 0)
        {
            //Debug.Log(rb.angularVelocity);
        }

        groundCheck.position = (Vector2)trans.position - Vector2.up * 0.4f;

        horInputValue = Input.GetAxisRaw(horInputName);

        Collider2D[] overlap = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer.value);
        if (overlap.Length > 1)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (Juggle.instance != null)
        {
            if (Juggle.instance.player != playerNumber && Array.IndexOf(overlap, Juggle.instance.col) != -1)
            {
                canJump = false;
            }
        }

        if (Input.GetButtonDown(jumpButtonName) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }

        //if (playerNumber == 0) Debug.Log(canJump);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * horInputValue * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    public void Respawn(Vector3 position)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 5f;

        float difference = playerNumber == 0 ? -2.5f : 2.5f;
        transform.position = position + Vector3.right * difference;
    }
}
