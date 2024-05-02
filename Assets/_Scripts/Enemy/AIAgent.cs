using System;
using UnityEngine;
using Pathfinding;

public class AIAgent : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;

    public bool canMove = false;

    private void Start()
    {
        path = GetComponent<AIPath>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (canMove)
        {
            path.maxSpeed = moveSpeed;
            path.destination = player.position;
        }
        else
        {
            path.maxSpeed = 0;
        }
    }
}