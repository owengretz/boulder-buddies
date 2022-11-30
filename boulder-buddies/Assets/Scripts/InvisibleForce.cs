using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleForce : MonoBehaviour
{
    //private SpriteRenderer rend;
    public ParticleSystem rope;

    public Rigidbody2D playerOne;
    public Rigidbody2D playerTwo;

    private bool pulling;
    public float force;
    public float maxDistance;

    private void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        //rope = GetComponent<ParticleSystem>().shape;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.restarting)
            return;

        float distance = (playerOne.position - playerTwo.position).magnitude;

        if (distance > maxDistance)
        {
            Vector2 dir = (playerTwo.position - playerOne.position).normalized;

            playerOne.AddForce(dir * force * Time.fixedDeltaTime, ForceMode2D.Impulse);
            playerTwo.AddForce(-dir * force * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        float dist = (playerOne.position - playerTwo.position).magnitude;

        //rend.size = new Vector2((playerOne.position - playerTwo.position).magnitude - 1f, rend.size.y);
        ParticleSystem.ShapeModule shape = rope.shape;
        shape.scale = new Vector3(dist - 1f, 0.1f, 0f);

        ParticleSystem.EmissionModule emission = rope.emission;
        emission.rateOverTime = 100f * dist;

        Color newCol = Color.black;
        bool switchCol = false;

        if (!pulling && dist > maxDistance)
        {
            newCol = new Color(1f, 0.3726415f, 0.4600175f);
            switchCol = true;
            pulling = true;
        }
        else if (pulling && dist < maxDistance)
        {
            newCol = new Color(1f, 0.9712749f, 0.3537736f);
            switchCol = true;
            pulling = false;
        }

        if (switchCol)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[rope.particleCount];
            rope.GetParticles(particles);

            if (particles != null)
            {
                for (int p = 0; p < particles.Length; p++)
                {
                    particles[p].startColor = newCol;
                }
                rope.SetParticles(particles, particles.Length);
            }
            ParticleSystem.MainModule main = rope.main;
            main.startColor = newCol;
        }



        Vector2 dir = (playerTwo.position - playerOne.position).normalized;
        transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        transform.position = (playerTwo.position + playerOne.position) / 2f;
    }
}
