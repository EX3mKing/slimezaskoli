using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    private PlayerInputActions input;
    private InputAction input_menu;
    private InputAction input_weapon_switch;

    [SerializeField] private GameObject volume_target;
    [SerializeField] private GameObject weapon_switch_target;

    [SerializeField] private GameObject volume_select;
    [SerializeField] private GameObject weapon_select;

    [SerializeField] private GameObject event_system_object;
    private EventSystem event_system;


    private void Awake()
    {
        event_system = event_system_object.GetComponent<EventSystem>();
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input_menu = input.Player.Menu;
        input_weapon_switch = input.Player.SwitchWeapons;
        input_menu.Enable();
        input_weapon_switch.Enable();
    }

    private void OnDisable()
    {
        input_menu.Disable();
        input_weapon_switch.Disable();
    }

    private void Update()
    {
        if (input_menu.triggered && !weapon_switch_target.activeSelf)
        {
            if (volume_target.activeSelf)
            {
                DisableObject();
            }
            else
            {
                EnableObject();
                event_system.SetSelectedGameObject(volume_select);
            }
        }

        if (input_weapon_switch.triggered && !volume_target.activeSelf)
        {
            if (weapon_switch_target.activeSelf)
            {
                DisableSwitch();
            }
            else
            {
                EnableSwitch();
                event_system.SetSelectedGameObject(weapon_select);
            }
        }
    }

    // USE ONLY FOR VOLUME
    public void DisableObject()
    {
        volume_target.SetActive(false);
        Time.timeScale = 1f;
    }

    // USE ONLY FOR VOLUME - nezelim nista u sceni zeznut
    public void EnableObject()
    {
        volume_target.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisableSwitch()
    {
        weapon_switch_target.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnableSwitch()
    {
        weapon_switch_target.SetActive(true);
        Time.timeScale = 0f;
    }
}
