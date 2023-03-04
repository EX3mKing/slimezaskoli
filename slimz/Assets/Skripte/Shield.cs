using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement movement;

    private void Update()
    {
        if (movement.input_move_direction.x != 0f && Time.timeScale > 0.125f)
        {
            transform.localScale = (movement.input_move_direction.x > 0f)
                ? new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y)
                : new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        transform.position = player.transform.position;
    }
}
