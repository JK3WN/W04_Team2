using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Canvas canvas;
    public GameObject buttonPrefab;
    public GameObject currentButton;
    public GameObject hpTextPrefab;
    public GameObject hpUI;
    public TextMeshProUGUI hpText;

    public TurnManager turnManager;

    //public ShipManager shipManager;

    public virtual void Start()
    {
        position = new Vector2(transform.position.x, transform.position.y);
        attackPositions = new List<Vector2>();

        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        if (turnManager != null) turnManager.WeightStart += OnWeightStart;

        canvas = GameObject.FindWithTag("ShipUI").GetComponent<Canvas>();
        
        currentHP = weight;
        CreateHP();
    }

    private void OnDisable()
    {
        if (turnManager != null) turnManager.WeightStart -= OnWeightStart;
    }

    private void OnWeightStart(int CallWeight)
    {
        if (weight != CallWeight) return;
        // YJK, 자기 weight와 동일한 event가 들어올 때만 진행
        Attack();
        if (clicked)
        {
            clicked = false;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(currentButton);
        }
        Invoke("DeathCheck", 0.3f);
    }

    public virtual void Attack() // attack
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

    public virtual void DeathCheck() // death
    {
        if (currentHP <= 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(currentButton);
            Destroy(hpUI);
            Destroy(gameObject);
        }
    }

    public virtual void Damaged(int damage) // damage
    {
        currentHP -= damage;
        ShowHP();
    }

    public virtual void Rotate()
    {
        transform.Rotate(0, 0, 90);
        attackDir = - transform.up;
        Debug.Log(attackDir);
    }

    public virtual void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (clicked)
            {
                clicked = false;
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                Destroy(currentButton);
            }
            else
            {
                clicked = true;
                ShowAttackRange();
                ShowButton();
            }
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

        ButtonScript buttonScript = currentButton.GetComponent<ButtonScript>();
        buttonScript.ship = this;

        RectTransform buttonRectTransform = currentButton.GetComponent<RectTransform>();
        buttonRectTransform.position = transform.position + new Vector3(0, 1, 0);
    }

    public virtual void CreateHP()
    {
        hpUI = Instantiate(hpTextPrefab);

        hpUI.transform.SetParent(canvas.transform, false);

        hpText = hpUI.GetComponent<TextMeshProUGUI>();
        RectTransform hpUITransform = hpUI.GetComponent<RectTransform>();
        hpUITransform.position = transform.position + new Vector3(0, -0.2f, 0);

        ShowHP();
    }

    public virtual void ShowHP()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP.ToString();
        }
        else
        {
            Debug.LogWarning("hpText is not assigned.");
        }
    }
}
