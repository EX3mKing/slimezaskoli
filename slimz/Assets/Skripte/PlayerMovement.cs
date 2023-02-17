using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float input_activation_strength = 0.2f;
    [Range(0f, 1f)]
    [SerializeField] private float input_strength_for_jump = 0.5f;
    
    [SerializeField] private float speed_base_floor = 5f;
    [SerializeField] private float speed_base_air = 5f;
    [SerializeField] private float jump_base;

    [Header("Collider References")] 
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private BoxCollider2D collider_base;
    [SerializeField] private BoxCollider2D collider_floor;

    private bool hitting_base;
    private bool hitting_floor;

    private PlayerInputActions input_map;
    private InputAction movement;

    private Vector2 input_direction;
    
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
        // read the input direction and bind it to 1,1 0,1 1,0 0,0.. format
        input_direction = movement.ReadValue<Vector2>();

        if (input_direction.x > input_activation_strength) input_direction.x = 1f;
        if (input_direction.x < -input_activation_strength) input_direction.x = -1f;
        if (input_direction.y > input_activation_strength) input_direction.y = 1f;
        if (input_direction.y < -input_activation_strength) input_direction.y = -1f;
        
        if (Mathf.Abs(input_direction.x) < input_activation_strength) input_direction.x = 0f;
        if (Mathf.Abs(input_direction.y) < input_activation_strength) input_direction.y = 0f;
    }

    private void FixedUpdate()
    {
        hitting_base = ColliderCheck(collider_base, ground_layer);
        hitting_floor = ColliderCheck(collider_floor, ground_layer);
        
        // logic while on floor;
        if (hitting_base && hitting_floor)
        {
            player_rb.velocity = new Vector2(input_direction.x * speed_base_floor, player_rb.velocity.y);
        }
        
        // jump logic
        if (hitting_base && hitting_floor && input_direction.y > input_strength_for_jump)
        {
            player_rb.AddForce(Vector2.up * jump_base, ForceMode2D.Impulse);
        }
        
        // in air logic
        if (!hitting_floor)
        {
            player_rb.velocity = new Vector2(input_direction.x * speed_base_floor, player_rb.velocity.y);
        }
    }

    private bool ColliderCheck(BoxCollider2D col, LayerMask lm)
    {
        return Physics2D.OverlapBox(col.bounds.center, col.size, 0f, lm);
    }
}
