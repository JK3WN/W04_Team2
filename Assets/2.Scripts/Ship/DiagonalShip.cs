using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalShip : ShipBase
{
    public override void Start()
    {
        base.Start();

        weight = 2;
        currentHP = weight;
        attackDir = Vector2.down + Vector2.right;
        attackPositions.Add(position + attackDir);
        attackPositions.Add(position - attackDir);
    }

    public override void Attack()
    {
        base.Attack();
        //GameObject attackTile = Instantiate(attackTilePrefab, new Vector3(attackPositions[0].x, attackPositions[0].y, 0), Quaternion.identity);
    }

    public override void Rotate()
    {
        base.Rotate();
    }

    public override void ResetAttackRange()
    {
        base.ResetAttackRange();
        attackDir = -transform.up + transform.right;
        attackPositions.Clear();
        attackPositions.Add(position + attackDir);
        attackPositions.Add(position - attackDir);
    }
}
