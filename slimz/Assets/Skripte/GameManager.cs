using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public AudioSource music_source, sfx_source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // audio section
    public void PlaySFX(AudioClip clip)
    {
        sfx_source.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        music_source.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
        Debug.Log(value);
    }

    public void ChangeMusicVolume(float value)
    {
        music_source.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        sfx_source.volume = value;
    }
}
