using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health_current;
    [SerializeField] private int health_max;

    public Sprite full_hearth;
    public Sprite empty_hearth;
    
    [SerializeField] private Image[] hearths;

    [SerializeField] private float invincible_duration;
    private float invincible_current;

    private void Update()
    {
        if (invincible_current > 0) invincible_current -= Time.deltaTime;
    }

    public void TakeDMG(int dmg, string type)
    {
        if (invincible_current <= 0f)
        {
            if (health_current - dmg > 0)
            {
                health_current -= dmg;
                invincible_current = invincible_duration;

                for (int i = health_max; i > health_current; i--)
                {
                    hearths[i-1].sprite = empty_hearth;
                }
            }
            else
            {
                GameManager.Instance.Loose("Died of: " + type);
                GameManager.Instance.death_reason = type;
            }
        }
    }
}
