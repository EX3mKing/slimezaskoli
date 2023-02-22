using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health_current;
    [SerializeField] private int health_max;

    [SerializeField] private float invincible_duration;
    private float invincible_current;

    private void Update()
    {
        if (invincible_current > 0) invincible_current -= Time.deltaTime;
    }

    public void TakeDMG(int dmg)
    {
        if (invincible_current <= 0f)
        {
            if (health_current - dmg > 0)
            {
                health_current -= dmg;
                invincible_current = invincible_duration;
            }
            else
            {
                Die();
            }
        }
    }

    private void Die()
    {
        GameManager.Instance.Loose("Taken Too Much Damage");
    }
}
