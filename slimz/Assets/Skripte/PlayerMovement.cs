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

    [SerializeField] private float speed_base_floor = 7f;
    [SerializeField] private float jump_strength_base = 13f;
    [SerializeField] private float coyote_time = 0.2f;
    [SerializeField] private float jump_buffer_time = 0.2f;
    [SerializeField] private float jump_ienumerator_callback_time = 0.4f;
    [Range(0f,1f)] [Tooltip("MORA BITI ISPOD 1 DA SE SMANJUJE")]
    [SerializeField] private float jump_falloff = 0.5f;

    [Header("Collider References")] 
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private BoxCollider2D collider_base;
    [SerializeField] private BoxCollider2D collider_floor;
    
    private Rigidbody2D player_rb;
    
    private PlayerInputActions input_map;
    private InputAction movement;
    private Vector2 input_move_direction;
    
    private bool hitting_base;
    private bool hitting_floor;

    private float coyote_time_current;
    private float jump_buffer_time_current;
    private bool is_jumping;


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
        input_move_direction = movement.ReadValue<Vector2>();

        if (input_move_direction.x > input_activation_strength) input_move_direction.x = 1f;
        if (input_move_direction.x < -input_activation_strength) input_move_direction.x = -1f;
        if (input_move_direction.y > input_activation_strength) input_move_direction.y = 1f;
        if (input_move_direction.y < -input_activation_strength) input_move_direction.y = -1f;
        
        if (Mathf.Abs(input_move_direction.x) < input_activation_strength) input_move_direction.x = 0f;
        if (Mathf.Abs(input_move_direction.y) < input_activation_strength) input_move_direction.y = 0f;
    }

    private void FixedUpdate()
    {
        hitting_base = ColliderCheck(collider_base, ground_layer);
        hitting_floor = ColliderCheck(collider_floor, ground_layer);
        
        /*
        // coyote time and jump buffer
        if (hitting_base && hitting_floor)
        {
            coyote_time_current = coyote_time;
        }
        else
        {
            if(coyote_time_current > 0f) coyote_time_current -= Time.deltaTime;
        }
        
        if (input_move_direction.y > 0f)
        {
            jump_buffer_time_current = jump_buffer_time;
        }
        else
        {
            if (jump_buffer_time_current > 0f) jump_buffer_time_current -= Time.deltaTime;
        }

        if (coyote_time_current > 0f && jump_buffer_time_current > 0f && !is_jumping)
        {
            player_rb.velocity = new Vector2(player_rb.velocity.x, jump_strength_base);
            jump_buffer_time_current = 0f;
            StartCoroutine(JumpCooldown());
        }

        if (input_move_direction.y > 0f && player_rb.velocity.y > 0f)
        {
            player_rb.velocity = new Vector2(player_rb.velocity.x, player_rb.velocity.y * jump_falloff);
            coyote_time_current = 0f;
        }
        */
        if (hitting_base && hitting_floor && input_move_direction.y > 0f)
        {
            player_rb.AddForce(Vector2.up * jump_strength_base, ForceMode2D.Impulse);
        }

        player_rb.velocity = new Vector2(input_move_direction.x * speed_base_floor, player_rb.velocity.y);
    }

    private bool ColliderCheck(BoxCollider2D col, LayerMask lm)
    {
        return Physics2D.OverlapBox(col.bounds.center, col.size, 0f, lm);
    }

    private IEnumerator JumpCooldown()
    {
        is_jumping = true;
        yield return new WaitForSeconds(jump_ienumerator_callback_time);
        is_jumping = false;
    }
}
