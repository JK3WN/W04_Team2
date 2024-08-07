using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiagonalShip : ShipBase
{

    public override void Start()
    {
        base.Start();
        weight = 2;
        currentHP = weight;
        attackDir = -transform.up + transform.right;
        for (int i = 1; i < 10; i++)
        {
            if ((position + attackDir * i).x < -5 || (position + attackDir * i).x > 5 || (position + attackDir * i).y < -5 || (position + attackDir * i).y > 5) break;
            attackPositions.Add(position + attackDir * i);
        }

    }

    public override void Attack()
    {

        for (int i = 0; i < attackPositions.Count; i++)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPositions[i], 0.1f);
            foreach (var collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Ship"))
                {
                    collider.gameObject.GetComponent<ShipBase>().Damaged(1);
                    return;
                }
                else if (collider.gameObject.CompareTag("Land"))
                {
                    //Destroy(collider.gameObject);
                    collider.gameObject.GetComponent<CreateDirt>().hp--;
                    return;
                }
            }
        }
    }

    public override void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (clicked)
            {
                clickOff();
                attackDir = -transform.up + transform.right;
                ResetAttackRange();
            }
            else
            {
                clickOff();
                clicked = true;
                tempRotation = transform.rotation;
                ShowAttackRange();
                if (TurnManager.currentTurn == team && GameManager.instance.ActionPoints > 0) ShowButton();
            }
        }
    }

    public override void Rotate()
    {
        base.Rotate();
        attackDir = -transform.up + transform.right;
        ResetAttackRange();
        ShowAttackRange();
    }

    public override void ShowAttackRange()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
        }

        for (int i = 0; i < attackPositions.Count; i++)
        {
            GameObject attackTile = Instantiate(attackTilePrefab, new Vector3(attackPositions[i].x, attackPositions[i].y, 0), Quaternion.identity);
            attackTile.transform.SetParent(transform);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPositions[i], 0.1f);
            foreach (var collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Ship") || collider.gameObject.CompareTag("Land")) return;

            }
        }
    }

    public override void ResetAttackRange()
    {
        base.ResetAttackRange();
        attackPositions.Clear();
        for (int i = 1; i < 10; i++)
        {
            if ((position + attackDir * i).x < -5 || (position + attackDir * i).x > 5 || (position + attackDir * i).y < -5 || (position + attackDir * i).y > 5) break;
            attackPositions.Add(position + attackDir * i);
        }
    }
}
