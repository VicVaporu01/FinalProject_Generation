using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction moveAction;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 moveDirection;

    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
    }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();

        rb2D.velocity = moveDirection * movementSpeed;
    }
}
