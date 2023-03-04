using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spear : MonoBehaviour
{
    private BoxCollider2D col;
    [SerializeField] private LayerMask player_mask;
    [SerializeField] private LayerMask terrain_mask;
    
    [SerializeField] private float speed_throw;
    [SerializeField] private float manual_callback;
    [SerializeField] private GameObject player;
    
    private PlayerInputActions input;
    private InputAction input_throw;
    public PlayerMovement input_movement;

    private Vector3 velocity;

    private bool thrown = false;
    private bool follow = true;
    private bool hitting_terrain;
    private bool hitting_player;

    private float manual_callback_cur;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        input = new PlayerInputActions();
        input_throw = input.Player.Fire;
    }

    private void OnEnable()
    {
        input_throw.Enable();
    }

    private void OnDisable()
    {
        input_throw.Disable();
    }

    private void Update()
    {
        if (!thrown && Time.timeScale > 0f)
        {
            if (input_movement.input_move_direction.x > 0.125f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            if(input_movement.input_move_direction.x < 0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
        
        if (input_throw.triggered)
        {
            if (hitting_terrain)
            {
                thrown = false;
                follow = true;
                col.enabled = false;
            }
            else if(!thrown)
            {
                if(input_movement.input_move_direction.x != 0f)
                {
                    follow = false;
                    thrown = true;
                    velocity = new Vector3(input_movement.input_move_direction.x * speed_throw, 0f, 0f);
                    manual_callback_cur = manual_callback;
                }
                else if(input_movement.input_move_direction.x == 0f)
                {
                    follow = false;
                    thrown = true;
                    velocity = (transform.localScale.x > 0) ? new Vector3(-speed_throw, 0f, 0f) :
                        new Vector3(speed_throw, 0f, 0f);
                    manual_callback_cur = manual_callback;
                }
            }
        }
        if (follow)
        {
            transform.position = player.transform.position;
        }
    }

    private void FixedUpdate()
    {
        hitting_terrain = Physics2D.OverlapBox(col.bounds.center, col.bounds.size, 0f, terrain_mask);
        hitting_player = Physics2D.OverlapBox(col.bounds.center, col.bounds.size, 0f, player_mask);
        
        transform.position += velocity * Time.fixedDeltaTime;

        if (hitting_terrain && col.enabled)
        {
            velocity = Vector3.zero;
        }

        if (thrown && !col.enabled)
        {
            if (!hitting_player)
            {
                col.enabled = true;
            }
        }

        if (thrown && !hitting_terrain)
        {
            manual_callback_cur -= Time.fixedDeltaTime;
            if (manual_callback_cur <= 0f)
            {
                thrown = false;
                follow = true;
                col.enabled = false;
            }
        }
    }

    public void Follow()
    {
        thrown = false;
        follow = true;
        col.enabled = false;
    }
}
