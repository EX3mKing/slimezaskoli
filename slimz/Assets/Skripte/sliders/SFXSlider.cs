using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = GameManager.Instance.sfx_source.volume;
        slider.onValueChanged.AddListener(val => GameManager.Instance.ChangeSFXVolume(val));
    }
}
