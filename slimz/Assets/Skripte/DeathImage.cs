using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathImage : MonoBehaviour
{
    [SerializeField] private Sprite arrow, fall;
    [SerializeField] private Image image;
    private void Start()
    {
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
                    Debug.Log(GameManager.Instance.death_reason);
                    break;
            }
        }
    }
}
