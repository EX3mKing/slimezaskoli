using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float time_between_shoots;
    private float current_time;

    public GameObject Arrow_reference;
    public Vector2 velocity_initial;
    public Vector2 gravity;
    public float alive_time;
    public int damage;
    public float velocity_falloff;

    private void FixedUpdate()
    {
        current_time += Time.fixedDeltaTime;

        if (current_time > time_between_shoots)
        {
            current_time = 0f;

            GameObject temp_arrow = Instantiate(Arrow_reference, transform.position, transform.rotation);
            Projectile temp_projectile = temp_arrow.GetComponent<Projectile>();
            temp_projectile.alive_time = alive_time;
            temp_projectile.gravity = gravity;
            temp_projectile.velocity_initial = velocity_initial;
            temp_projectile.damage = damage;
            temp_projectile.velocity_falloff = velocity_falloff;
        }
    }
}
