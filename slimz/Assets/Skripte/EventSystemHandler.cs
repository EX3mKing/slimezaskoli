using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EventSystemHandler : MonoBehaviour
{
    private EventSystem event_system;
    private PlayerInputActions input;
    private InputAction input_select;
    
    [SerializeField]
    private GameObject[] sliders;

    private void Awake()
    {
        input = new PlayerInputActions();
        event_system = GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        input_select = input.UI.Submit;
        input_select.Enable();
    }

    private void Update()
    {
        if (input_select.triggered)
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                if (event_system.currentSelectedGameObject == sliders[i])
                {
                    sliders[i].SendMessage("MuteToggle");
                }
            }
        }
    }
}
