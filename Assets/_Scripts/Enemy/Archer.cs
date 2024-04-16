using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, player);
        base.FollowPlayer(player);
    }
}