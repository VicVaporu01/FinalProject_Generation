using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction interactAction;

    [Header("Boundary Values")]
    [SerializeField] private int minStatValue = 1;
    [SerializeField] private int maxStatValue = 8;

    [Header("Object Boost Values")]
    public int speedStats;
    public int damangeStats;
    public int maxLifeStats;

    [Header("Interact Object Values")]
    [SerializeField] private Vector3 interactRangeOffset;
    [SerializeField] private Vector2 interactBoxValues;

    public event Action<int, int, int> OnStatsChanged;

    private void Awake()
    {
        interactAction = playerInput.actions["Interact"];
    }

    public void Start()
    {
        speedStats = minStatValue;
        damangeStats = minStatValue;
        maxLifeStats = minStatValue;
    }

    private void OnEnable()
    {
        interactAction.started += PickUpObject;
    }

    private void OnDisable()
    {
        interactAction.started -= PickUpObject;
    }

    private void PickUpObject(InputAction.CallbackContext context)
    {
        RaycastHit2D[] objectsTouched = Physics2D.BoxCastAll(transform.position + interactRangeOffset, interactBoxValues, 0f, Vector2.down);

        foreach (RaycastHit2D item in objectsTouched)
        {
            if (item.transform.gameObject.TryGetComponent(out CollectibleObject collectibleObject))
            {
                collectibleObject.Collect();
            }

            if (item.transform.gameObject.TryGetComponent(out ExitPortal exitPortal))
            {
                exitPortal.ExitLevel();
            }
        }
    }

    public void ModifyStatistics(int newSpeed, int newDamage, int newMaxLife)
    {
        speedStats = EvaluateStatistics(newSpeed, speedStats);
        damangeStats = EvaluateStatistics(newDamage, damangeStats);
        maxLifeStats = EvaluateStatistics(newMaxLife, maxLifeStats);

        // Notificar al PauseMenu sobre los cambios en las estad�sticas
        OnStatsChanged?.Invoke(speedStats, damangeStats, maxLifeStats);
    }

    private int EvaluateStatistics(int newValue, int changeValue)
    {
        int nuevoTotal = changeValue + newValue;

        if (nuevoTotal > maxStatValue)
        {
            nuevoTotal = maxStatValue;
        }
        else if (nuevoTotal < minStatValue)
        {
            nuevoTotal = minStatValue;
        }

        return nuevoTotal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position + interactRangeOffset, interactBoxValues);
    }
}
