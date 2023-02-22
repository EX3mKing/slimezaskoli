using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite_on;
    [SerializeField] private Sprite sprite_off;
    
    private void Start()
    {
        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener(val => GameManager.Instance.ChangeMasterVolume(val));
        image.sprite = AudioListener.pause ? sprite_off : sprite_on;
    }

    public void MuteToggle()
    {
        AudioListener.pause = !AudioListener.pause;
        image.sprite = AudioListener.pause ? sprite_off : sprite_on;
    }
}
