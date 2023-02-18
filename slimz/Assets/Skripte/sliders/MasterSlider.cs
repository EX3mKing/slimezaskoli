using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener(val => GameManager.Instance.ChangeMasterVolume(val));
    }
}
