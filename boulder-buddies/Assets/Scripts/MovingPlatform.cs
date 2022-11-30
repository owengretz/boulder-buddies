using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] pathMarkers;

    public float speed;
    public float minSpeed = 0.01f;
    public float maxSpeed = 6f;

    public bool pauseAtMarker;
    [ConditionalHide("pauseAtMarker", true)]
    public float pauseTime = 0.5f;

    private Rigidbody2D rb;

    private Vector2 targetPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.position = pathMarkers[0].position;

        StartCoroutine(Move(0));
    }


    private IEnumerator Move(int marker)
    {
        int nextMarker = marker + 1 == pathMarkers.Length ? 0 : marker + 1;

        Vector2 dir = (pathMarkers[nextMarker].position - (Vector3)rb.position).normalized;

        while ((rb.position - (Vector2)pathMarkers[nextMarker].position).magnitude > 0.05f)
        {
            float distToPrev = (rb.position - (Vector2)pathMarkers[marker].position).magnitude;
            float distToNext = (rb.position - (Vector2)pathMarkers[nextMarker].position).magnitude;

            rb.velocity = dir * Mathf.Clamp(Mathf.Sqrt(distToNext * distToPrev) * speed, minSpeed, maxSpeed);

            yield return null;
        }

        if (pauseAtMarker)
        {
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(pauseTime);
        }

        StartCoroutine(Move(nextMarker));
    }
}
