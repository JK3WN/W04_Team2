using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject attackTile = Instantiate(attackTilePrefab, new Vector3(attackPositions[0].x, attackPositions[0].y, 0), Quaternion.identity);
    }

    public override void DeathCheck()
    {
        base.DeathCheck();
    }

    public override void Damaged(int damage)
    {
        base.Damaged(damage);
    }

    public override void Rotate(Vector2 direction)
    {
        base.Rotate(direction);
        attackPositions.Clear();
        attackPositions.Add(position + attackDir);
    }

    //public override void ShowAttackRange()
    //{
    //    base.ShowAttackRange();
    //}
}
