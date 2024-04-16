using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    private void Start()
    {
        // followDistance = 2.0f;
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, player);
        base.FollowPlayer(player);
    }
}