using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StraightShip : ShipBase
{
    public override void Start()
    {
        base.Start();

        weight = 3;
        currentHP = weight;
        attackDir = -transform.up;
        
        for(int i = 1; i < 10; i++)
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
                    Instantiate(boomPrefab, attackPositions[i], Quaternion.identity);
                    collider.gameObject.GetComponent<ShipBase>().Damaged(1);
                    return;
                }
                else if (collider.gameObject.CompareTag("Land"))
                {
                    //Destroy(collider.gameObject);
                    Instantiate(boomPrefab, attackPositions[i], Quaternion.identity);
                    collider.gameObject.GetComponent<CreateDirt>().hp--;
                    return;
                }

            }
        }
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown(); 
        if (clicked)
        {
            clickOff();
            attackDir = -transform.up;
            ResetAttackRange();
        }
        else
        {
            clickOff();
            clicked = true;
            tempRotation = transform.rotation;
            ShowAttackRange();
            if (TurnManager.currentTurn == team && actionPoint > 0 && CheckSelected()) ShowButton();
            ShipPanel.SetActive(true);
            ShowShipInfo();
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
        ResetAttackRange();
        foreach (Transform child in transform)
        {
            if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
            lineRenderer.enabled = false;
        }

        if (attackPositions.Count < 1) return;

        for (int i = 0; i < attackPositions.Count; i++)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPositions[i], 0.1f);
            foreach (var collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Ship") || collider.gameObject.CompareTag("Land"))
                {
                    GameObject attackTile = Instantiate(attackTilePrefab, new Vector3(attackPositions[i].x, attackPositions[i].y, 0), Quaternion.identity);
                    attackTile.transform.SetParent(transform);
                    lineRenderer.enabled = true;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, attackPositions[i]);
                    return;
                }
            }
        }
        lineRenderer.enabled = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, attackPositions[attackPositions.Count - 1]);
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

    public override void ReAssignAttackDir()
    {
        base.ReAssignAttackDir();
        attackDir = -transform.up;
    }
}
