using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    private PlayerInputActions input;
    private InputAction input_menu;
    
    [SerializeField]
    private GameObject target;

    private void Awake()
    {
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input_menu = input.Player.Menu;
        input_menu.Enable();
    }

    private void OnDisable()
    {
        input_menu.Disable();
    }

    private void Update()
    {
        if (input_menu.triggered)
        {
            if (target.activeSelf)
            {
                DisableObject();
            }
            else
            {
                EnableObject();
            }

        }
    }

    public void DisableObject()
    {
        target.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnableObject()
    {
        target.SetActive(true);
        Time.timeScale = 0f;
    }
}
