using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 velocity_initial;
    public Vector2 gravity;
    public float alive_time;
    public DMG damage;
    public float velocity_falloff;

    private float alive_time_cur;
    private Rigidbody2D rb;
    private Vector2 accumulated_gravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity_initial;
        alive_time_cur = alive_time;
    }

    private void FixedUpdate()
    {
        velocity_initial -= velocity_initial * (velocity_falloff * Time.fixedDeltaTime);
        accumulated_gravity += gravity * Time.fixedDeltaTime;
        
        rb.velocity = accumulated_gravity + velocity_initial;
        
        alive_time_cur -= Time.fixedDeltaTime;
        
        if(alive_time_cur <= 0) Destroy(gameObject);
    }

    // just here if projectile is a physics obj
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            col.transform.SendMessage("TakeDMG", damage);
        }
        Destroy(gameObject);
    }

    // this is normally used
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("Hit at: " + col.name);
            col.transform.parent.parent.SendMessage("TakeDMG", damage);
        }
        Destroy(gameObject);
    }
}
