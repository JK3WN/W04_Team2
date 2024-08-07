using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicShip : ShipBase
{
    public override void Start()
    {
        base.Start();

        weight = 1;
        currentHP = weight;
        attackDir = Vector2.down;
        attackPositions.Add(position + attackDir);
    }

    public override void Attack()
    {
        base.Attack();
        //GameObject attackTile = Instantiate(attackTilePrefab, new Vector3(attackPositions[0].x, attackPositions[0].y, 0), Quaternion.identity);
    }
    public override void Rotate()
    {
        base.Rotate();
        attackDir = -transform.up;
    }

    public override void ResetAttackRange()
    {
        base.ResetAttackRange();
        attackDir = -transform.up;
        attackPositions.Clear();
        attackPositions.Add(position + attackDir);
    }
}
