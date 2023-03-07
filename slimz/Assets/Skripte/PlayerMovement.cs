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

    [Header("Movement essentials")]
    [SerializeField] private float speed_base_floor = 7f;
    [SerializeField] private float jump_strength_base = 13f;
    [SerializeField] private float gravity_base;
    [SerializeField] private float gravity_jumping;
    [SerializeField] private float coyote_time;

    [Header("References")] 
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private BoxCollider2D collider_base;
    [SerializeField] private BoxCollider2D collider_floor;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private AudioClip jump_clip;
    
    
    private Rigidbody2D player_rb;
    
    private PlayerInputActions input_map;
    private InputAction movement;
    public Vector2 input_move_direction;
    private float coyote_time_current;
    
    private bool hitting_base;
    private bool hitting_floor;

    private float last_height;

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
        // read the input direction and bind it to 11, 10, 01, 00.. format
        input_move_direction = movement.ReadValue<Vector2>();

        // activation strength is kinda useless bcs of new input system, can just replace with 0f
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
        
        // animation and sprite
        animator.SetBool("Grounded", hitting_base && hitting_floor);
        animator.SetBool("Moving", Mathf.Abs(input_move_direction.x) > 0f);
        animator.SetBool("Falling", last_height > player_rb.position.y);

        if (input_move_direction.x < 0f) renderer.flipX = false;
        if (input_move_direction.x > 0f) renderer.flipX = true;
        
        // coyote time
        if (hitting_base && hitting_floor)
        {
            coyote_time_current = coyote_time;
        }
        else
        {
            if (coyote_time_current > 0f) coyote_time_current -= Time.fixedDeltaTime;
        }
        
        // actual jump
        if ( coyote_time_current > 0f && input_move_direction.y > 0f)
        {
            player_rb.AddForce(Vector2.up * jump_strength_base, ForceMode2D.Impulse);
            coyote_time_current = 0f;
            GameManager.Instance.PlaySFX(jump_clip);
        }
        
        // basically fall
        if (!(hitting_base && hitting_floor))
        {
            if (input_move_direction.y > 0f)
            {
                player_rb.velocity = new Vector2(player_rb.velocity.x,
                    player_rb.velocity.y - gravity_jumping * Time.fixedDeltaTime);
            }
            else
            {
                player_rb.velocity = new Vector2(player_rb.velocity.x,
                    player_rb.velocity.y - gravity_base * Time.fixedDeltaTime);
            }
        }
        
        // needed for animation
        last_height = transform.position.y;
        
        // moves left or right
        player_rb.velocity = new Vector2(input_move_direction.x * speed_base_floor, player_rb.velocity.y);
    }

    private bool ColliderCheck(BoxCollider2D col, LayerMask lm)
    {
        return Physics2D.OverlapBox(col.bounds.center, col.size, 0f, lm);
    }
}