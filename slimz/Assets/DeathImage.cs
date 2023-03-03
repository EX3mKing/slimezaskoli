using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathImage : MonoBehaviour
{
    [SerializeField] private Sprite arrow, fall;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        
        if (GameManager.Instance.death_reason != null)
        {
            switch (GameManager.Instance.death_reason)
            {
                case "arrow":
                    image.sprite = arrow;
                    break;
                case "fall":
                    image.sprite = fall;
                    break;
                default:
                    break;
            }
        }
    }
}
