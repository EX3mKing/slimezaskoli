using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = GameManager.Instance.music_source.volume;
        slider.onValueChanged.AddListener(val => GameManager.Instance.ChangeMusicVolume(val));
    }
}
