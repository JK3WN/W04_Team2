using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewDiagonalShip : ShipBase
    {

        public override void Start()
        {
            base.Start();
            weight = 3;
            currentHP = weight;
            attackDir = -transform.up - transform.right;
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
            if (clicked)
            {
                clickOff();
                attackDir = -transform.up - transform.right;
                ResetAttackRange();
            }
            else
            {
                clickOff();
                clicked = true;
                tempRotation = transform.rotation;
                ShowAttackRange();
                if (TurnManager.currentTurn == team && actionPoint > 0) ShowButton();
                ShipPanel.SetActive(true);
                ShowShipInfo();
            }
        }

        public override void Rotate()
        {
            base.Rotate();
            attackDir = -transform.up - transform.right;
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

    public override void ReAssignAttackDir()
    {
        base.ReAssignAttackDir();
        attackDir = -transform.up - transform.right;
    }
}

