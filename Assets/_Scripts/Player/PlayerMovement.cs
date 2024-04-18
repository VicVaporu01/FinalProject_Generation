using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction attack;
    

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 moveDirection;

    Animator anims;

    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
        attack = playerInput.actions["Attack"];
        anims = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();

        anims.SetFloat("MovementX", moveDirection.x);
        anims.SetFloat("MovementY", moveDirection.y);
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            anims.SetFloat("LastMovX", moveDirection.x);
            anims.SetFloat("LastMovY", moveDirection.y);
        }
        rb2D.velocity = moveDirection * movementSpeed;
        if (attack.WasPressedThisFrame() && attack.IsPressed())
        {
            Debug.Log("Precionado");
            anims.SetTrigger("attack");
        }
        
    }

}
