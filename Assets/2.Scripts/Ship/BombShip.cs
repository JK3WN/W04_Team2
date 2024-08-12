using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShip : ShipBase
{
    public override void Start()
    {
        base.Start();

        weight = 3;
        currentHP = weight;
        ResetAttackRange();
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
    public override void OnMouseDown()
    {
        if (clicked)
        {
            clickOff();
            ResetAttackRange();
        }
        else
        {
            clickOff();
            clicked = true;
            tempRotation = transform.rotation;
            ShowAttackRange();
            // if (TurnManager.currentTurn == team && actionPoint > 0 && CheckSelected()) ShowButton();
            if (TurnManager.currentTurn == team && TurnManager.usedAP < 2 && CheckSelected()) ShowButton();
            //ShipPanel.SetActive(true);
            ShowShipInfo();
        }
    }

    public override void ResetAttackRange()
    {
        base.ResetAttackRange();
        attackPositions.Clear();
        attackDir = transform.up;
        attackPositions.Add(position + attackDir);
        attackDir = transform.up + transform.right;
        attackPositions.Add(position + attackDir);
        attackDir = transform.up - transform.right;
        attackPositions.Add(position + attackDir);
        attackDir = transform.right;
        attackPositions.Add(position + attackDir);

        attackDir = -transform.up;
        attackPositions.Add(position + attackDir);
        attackDir = -transform.up + transform.right;
        attackPositions.Add(position + attackDir);
        attackDir = -transform.up - transform.right;
        attackPositions.Add(position + attackDir);
        attackDir = -transform.right;
        attackPositions.Add(position + attackDir);
    }
}