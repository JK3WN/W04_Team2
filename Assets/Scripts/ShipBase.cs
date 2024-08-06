using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipBase : MonoBehaviour
{
    public int weight;
    public int currentHP;
    public Vector2 position;
    public Vector2 attackDir;
    public List<Vector2> attackPositions;
    public GameObject attackTilePrefab;
    public bool clicked;
    public GameObject buttonPrefab;
    public Canvas canvas;
    public GameObject currentButton;


    public virtual void Start()
    {
        position = new Vector2(transform.position.x, transform.position.y);
        attackPositions = new List<Vector2>();
    }
    
    public virtual void Attack() // attack
    {

    }
    public virtual void DeathCheck() // death
    {
        if (currentHP < 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Damaged(int damage) // damage
    {
        currentHP -= damage;
    }

    public virtual void Rotate(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        attackDir = direction;
    }

    public virtual void OnMouseDown()
    {
        if(clicked)
        {
            clicked = false;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            clicked = true;
            ShowAttackRange();
        }
        
    }
    public virtual void ShowAttackRange()
    {
        GameObject attackTile = Instantiate(attackTilePrefab, new Vector3(attackPositions[0].x, attackPositions[0].y, 0), Quaternion.identity);
        attackTile.transform.SetParent(transform);
    }

    public virtual void ShowButton()
    {
        currentButton = Instantiate(buttonPrefab);

        currentButton.transform.SetParent(canvas.transform, false);

        RectTransform buttonRectTransform = currentButton.GetComponent<RectTransform>();
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        buttonRectTransform.position = screenPoint + new Vector2(100, 0); // 오브젝트의 오른쪽으로 100픽셀 이동
    }
}
