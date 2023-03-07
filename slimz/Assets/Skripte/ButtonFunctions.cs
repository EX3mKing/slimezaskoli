using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void PlaySfx(AudioClip clip)
    {
        GameManager.Instance.PlaySFX(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        GameManager.Instance.PlayMusic(clip);
    }
}
