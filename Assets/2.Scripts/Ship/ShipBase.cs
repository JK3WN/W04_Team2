using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBase : MonoBehaviour
{
    public int weight;
    public int currentHP;
    public TurnList team;

    protected Vector2 position;
    protected Vector2 attackDir;
    protected List<Vector2> attackPositions;
    public GameObject attackTilePrefab;

    public bool clicked;
    protected Quaternion tempRotation;

    public Canvas canvas;
    public GameObject buttonPrefab;
    public GameObject currentButton;
    public GameObject checkButtonPrefab;
    protected GameObject currentCheckButton;
    public GameObject hpTextPrefab;
    protected GameObject hpUI;
    protected TextMeshProUGUI hpText;

    protected TurnManager turnManager;
    public GameObject ShipPanel;

    //public ShipManager shipManager;

    private void Awake()
    {
        // YJK, 함선 정보 보여줄 UI 참조
        if (ShipPanel == null) ShipPanel = GameObject.Find("ShipPanel");
    }

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
        if (clicked)
        {
            clicked = false;
            transform.rotation = tempRotation;
            foreach (Transform child in transform)
            {
                if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
            }
            Destroy(currentButton);
            Destroy(currentCheckButton);
        }
        Invoke("DeathCheck", 0.1f);

        if (weight == CallWeight) Attack();
        
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
    }

    public virtual void OnMouseDown()
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
                Destroy(currentButton);
                Destroy(currentCheckButton);
                ShipPanel.SetActive(false);
            }
            else
            {
                clicked = true;
                tempRotation = transform.rotation;
                ShowAttackRange();
                ShowButton();
                ShipPanel.SetActive(true);
                ShowShipInfo();
            }
        }
    }
    public virtual void ShowAttackRange()
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

    public virtual void ShowButton()
    {
        currentButton = Instantiate(buttonPrefab);
        currentCheckButton = Instantiate(checkButtonPrefab);

        currentButton.transform.SetParent(canvas.transform, false);
        currentCheckButton.transform.SetParent(canvas.transform, false);

        ButtonScript buttonScript = currentButton.GetComponent<ButtonScript>();
        buttonScript.ship = this;
        CheckButton checkButton = currentCheckButton.GetComponent<CheckButton>();
        checkButton.ship = this;

        RectTransform buttonRectTransform = currentButton.GetComponent<RectTransform>();
        buttonRectTransform.position = transform.position + new Vector3(-0.1f, 0.7f, 0);
        RectTransform checkRectTransform = currentCheckButton.GetComponent<RectTransform>();
        checkRectTransform.position = transform.position + new Vector3(0.5f, 0.7f, 0);
       
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

    public virtual void ResetAttackRange()
    {
    }

    public virtual void RepositionUI()
    {
        position = transform.position; 
        RectTransform hpUITransform = hpUI.GetComponent<RectTransform>();
        hpUITransform.position = transform.position + new Vector3(0, -0.2f, 0);
        ResetAttackRange();
    }

    public void ShowShipInfo()
    {
        ShipPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        ShipPanel.transform.Find("ShipImage").GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        ShipPanel.transform.Find("ShipImage").GetComponent<Image>().SetNativeSize();
        ShipPanel.transform.Find("StatText").GetComponent<TextMeshProUGUI>().text = currentHP + " / " + weight + "\n" + (5 - weight);
    }
}
