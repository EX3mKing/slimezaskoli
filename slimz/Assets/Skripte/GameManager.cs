using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource music_source, sfx_source;
    public string death_reason = "null";

    private void Awake()
    {
        // only single instance
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

    #region audio

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
    }

    public void ChangeMusicVolume(float value)
    {
        music_source.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        sfx_source.volume = value;
    }

    #endregion

    #region death

    public void Loose(string reason)
    {
        death_reason = reason;
        SceneManager.LoadScene(2);
        Debug.Log(reason);
    }

    #endregion

    #region win

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

    #endregion
}
