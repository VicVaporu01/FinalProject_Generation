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

    [Header("References")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Vector2 lastMoveDirection;
    [SerializeField] private float minMoveSpeed;
    [SerializeField] private float maxMoveSpeed;

    Animator anims;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
        attack = playerInput.actions["Attack"];
        anims = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerStats.OnStatsChanged += ChangeMovementSpeed;
    }

    private void OnDisable()
    {
        playerStats.OnStatsChanged -= ChangeMovementSpeed;
    }

    private void Start()
    {
        movementSpeed = playerStats.GetNewStatValue(GameManager.Instance.speedStats, maxMoveSpeed, minMoveSpeed);
    }

    private void ChangeMovementSpeed(int newSpeed, int newDamage, int newMaxLife, int newMagicDamage, int newBulletAmountStats)
    {
        movementSpeed = playerStats.GetNewStatValue(newSpeed, maxMoveSpeed, minMoveSpeed);
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
            lastMoveDirection = new Vector2(moveDirection.x, moveDirection.y);
        }
        rb2D.velocity = moveDirection * movementSpeed;
        if (attack.WasPressedThisFrame() && attack.IsPressed())
        {
            MeleeAttack();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Damaged();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RangeAttack();
        }
    }
    public void MeleeAttack()
    {
        int attackAnim = Random.Range(0, 3);
        anims.SetTrigger("Attack");
        anims.SetInteger("AttackAnim", attackAnim);
        // Debug.Log(attackAnim);
    }
    public void Damaged()
    {
        anims.SetTrigger("Damaged");
    }
    public void RangeAttack()
    {
        anims.SetTrigger("RangeAttack");
    }

    public void StopPlayerMovement()
    {
        rb2D.velocity = new Vector2(0, 0);
    }

    public Vector2 GetLasMovement()
    {
        return lastMoveDirection;
    }
}
