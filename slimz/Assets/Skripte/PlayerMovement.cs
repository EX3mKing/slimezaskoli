using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed_base;
    private InputAction movement;
    private PlayerInputActions input_map;
    private Rigidbody2D player_rb;

    private void Awake()
    {
        input_map = new PlayerInputActions();
        player_rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        movement = input_map.Player.Move;
        
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    private void Update()
    {
        player_rb.velocity = movement.ReadValue<Vector2>() * speed_base;
    }
}
