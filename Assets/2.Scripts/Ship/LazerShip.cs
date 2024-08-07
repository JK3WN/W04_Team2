using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class LazerShip : ShipBase
{
    public override void Start()
    {
        base.Start();

        weight = 4;
        currentHP = weight;
        attackDir = -transform.up;
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
                }

            }
        }
    }

    public override void OnMouseDown()
    {
        clickOff();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (clicked)
            {
                clicked = false;
                transform.rotation = tempRotation;
                attackDir = -transform.up;
                ResetAttackRange();
                foreach (Transform child in transform)
                {
                    if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
                }
                Destroy(currentButton);
                Destroy(currentCheckButton);
            }
            else
            {
                clicked = true;
                tempRotation = transform.rotation;
                ShowAttackRange();
                ShowButton();
            }
        }


    }

    public override void Rotate()
    {
        base.Rotate();
        attackDir = -transform.up;
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
