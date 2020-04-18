using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    private bool isFlying = false;
    private Vector3 movement = Vector3.zero;

    private void HandleInputs()
    {
        if (Input.GetButtonDown("Fly"))
        {
            isFlying = !isFlying;
        }
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        movement.x = horizontalMovement;
        movement.z = verticalMovement;
        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }
    }

    private void Move()
    {
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        HandleInputs();
        Move();
    }
}
