using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite_on;
    [SerializeField] private Sprite sprite_off;

    private void Start()
    {
        slider.value = GameManager.Instance.sfx_source.volume;
        slider.onValueChanged.AddListener(val => GameManager.Instance.ChangeSFXVolume(val));
        image.sprite = GameManager.Instance.sfx_source.mute ? sprite_off : sprite_on;
    }
    public void MuteToggle()
    {
        GameManager.Instance.sfx_source.mute = !GameManager.Instance.sfx_source.mute;
        image.sprite = GameManager.Instance.sfx_source.mute ? sprite_off : sprite_on;
    }
}
