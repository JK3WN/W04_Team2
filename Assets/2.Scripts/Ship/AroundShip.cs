using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class AroundShip : ShipBase
{
    public override void Start()
    {
        base.Start();

        weight = 3;
        currentHP = weight;
        attackPositions.Add(position + Vector2.down);
        attackPositions.Add(position + Vector2.up);
        attackPositions.Add(position + Vector2.right);
        attackPositions.Add(position + Vector2.left);
        attackPositions.Add(position + Vector2.down + Vector2.right);
        attackPositions.Add(position + Vector2.up + Vector2.left);
        attackPositions.Add(position + Vector2.right + Vector2.up);
        attackPositions.Add(position + Vector2.left + Vector2.down);
    }
    public override void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (clicked)
            {
                clicked = false;
                transform.rotation = tempRotation;
                foreach (Transform child in transform)
                {
                    if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
                }
                //Destroy(currentButton);
                //Destroy(currentCheckButton);
            }
            else
            {
                clicked = true;
                tempRotation = transform.rotation;
                ShowAttackRange();
                //ShowButton();
            }
        }
    }

    public override void ResetAttackRange()
    {
        base.ResetAttackRange();
        attackPositions.Clear();
        attackPositions.Add(position + Vector2.down);
        attackPositions.Add(position + Vector2.up);
        attackPositions.Add(position + Vector2.right);
        attackPositions.Add(position + Vector2.left);
        attackPositions.Add(position + Vector2.down + Vector2.right);
        attackPositions.Add(position + Vector2.up + Vector2.left);
        attackPositions.Add(position + Vector2.right + Vector2.up);
        attackPositions.Add(position + Vector2.left + Vector2.down);
    }
}
