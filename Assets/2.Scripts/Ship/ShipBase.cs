using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBase : MonoBehaviour
{
    public int weight;
    public int currentHP;
    public int actionPoint;
    public TurnList team;

    protected Vector2 position;
    protected Vector2 attackDir;
    protected List<Vector2> attackPositions;
    public GameObject attackTilePrefab;

    public bool clicked;
    public Quaternion tempRotation;

    public Canvas canvas;
    public GameObject buttonPrefab;
    public GameObject currentButton;
    public GameObject checkButtonPrefab;
    protected GameObject currentCheckButton;
    public GameObject hpTextPrefab;
    protected GameObject hpUI;
    protected TextMeshProUGUI hpText;
    public GameObject arrowButtonPrefab;
    public GameObject currentArrowButton;

    protected TurnManager turnManager;
    public GameObject ShipPanel;

    public List<Sprite> shipSpriteList;
    protected SpriteRenderer shipSprite;

    public GameObject boomPrefab;

    //public ShipManager shipManager;

    public LayerMask mask;

    public Transform upPos;
    private bool canMove;

    public bool selected;
    public LineRenderer lineRenderer;
    private void Awake()
    {
        // YJK, 함선 정보 보여줄 UI 참조
        if (ShipPanel == null) ShipPanel = GameObject.Find("ShipPanel");
    }

    public virtual void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shipSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (team == TurnList.P1) shipSprite.sprite = shipSpriteList[0];
        else shipSprite.sprite = shipSpriteList[1];

        position = new Vector2(transform.position.x, transform.position.y);
        attackPositions = new List<Vector2>();

        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        if (turnManager != null) turnManager.WeightStart += OnWeightStart;

        canvas = GameObject.FindWithTag("ShipUI").GetComponent<Canvas>();
        
        currentHP = weight;
        actionPoint = 2;
        selected = false;
        CreateHP();
    }

    private void OnDisable()
    {
        if (turnManager != null) turnManager.WeightStart -= OnWeightStart;
    }

    private void OnWeightStart(int CallWeight)
    {
        clickOff();
        ResetAttackRange();
        Invoke("DeathCheck", 0.1f);

        //if (weight == CallWeight) Attack();

        if (TurnManager.currentTurn == (team+1)) Attack(); // 공격권?
        actionPoint = 2;
        selected = false;
    }

    public virtual void Attack() // attack
    {
        for (int i = 0; i < attackPositions.Count; i++)
        {
            Instantiate(boomPrefab, attackPositions[i], Quaternion.identity);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPositions[i], 0.1f);
            foreach (var collider in hitColliders)
            {
                Debug.Log(collider);
                if (collider.gameObject.CompareTag("Ship"))
                {
                    collider.gameObject.GetComponent<ShipBase>().Damaged(1);
                }
                else if (collider.gameObject.CompareTag("Land"))
                {
                    //Destroy(collider.gameObject);
                    collider.gameObject.GetComponent<CreateDirt>().hp--;
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
        shipSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (team == TurnList.P1 && currentHP > 0)
        {
            shipSprite.sprite = shipSpriteList[(weight - currentHP) * 2];
        }
        else if (team == TurnList.P2 && currentHP > 0)
        {
            shipSprite.sprite = shipSpriteList[(weight - currentHP) * 2 + 1];
        }
        ShowHP();
    }

    public virtual void Rotate()
    {
        transform.Rotate(0, 0, 90);
        SetArrowButton();
        if (tempRotation != transform.rotation) currentArrowButton.SetActive(false);
        else currentArrowButton.SetActive(true);
    }


    public void Move()
    {
        canMove = true;
        actionPoint -= 1;
        selected = true;
        clickOff();
        // Use the ship's local downward direction
        Vector2 downDirection = transform.TransformDirection(Vector2.down);
        RaycastHit2D ray = Physics2D.Raycast(upPos.position, downDirection, 100f, mask);

        // Draw the ray in the scene view for debugging
        Debug.DrawLine(upPos.position, upPos.position + (Vector3)downDirection * 100f, Color.red, 2f);

        StartCoroutine(MoveToPos(ray));

    }
    IEnumerator MoveToPos(RaycastHit2D rays)
    {
        while (canMove)
        {
            yield return null;
            if (rays.collider != null)
            {
                // Draw the current position to the target position
                Debug.DrawLine(upPos.position, rays.point, Color.green);

                float length = Mathf.Round(Vector2.Distance(transform.position, rays.point));

                if (rays.collider.CompareTag("Dirt"))
                {
                    if (Mathf.Round(Vector2.Distance(transform.position, rays.collider.transform.position)) == 0)
                    {
                        canMove = false;
                        transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f);
                        break;
                    }
                }
                else if (!rays.collider.CompareTag("Dirt") && Mathf.Round(Vector2.Distance(transform.position, rays.point)) == 0)
                {
                    canMove = false;
                    transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f);
                    break;
                }

                transform.Translate(Vector2.down * 10 * Time.deltaTime);
                
            }
        }
        RepositionUI();
    }

    public virtual void OnMouseDown()
    {

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
        currentArrowButton = Instantiate(arrowButtonPrefab);

        currentButton.transform.SetParent(canvas.transform, false);
        currentCheckButton.transform.SetParent(canvas.transform, false);
        currentArrowButton.transform.SetParent(canvas.transform, false);

        ButtonScript buttonScript = currentButton.GetComponent<ButtonScript>();
        buttonScript.ship = this;
        CheckButton checkButton = currentCheckButton.GetComponent<CheckButton>();
        checkButton.ship = this;
        ArrowButton arrowButton = currentArrowButton.GetComponent<ArrowButton>();
        arrowButton.ship = this;

        SetArrowButton();
        RectTransform buttonRectTransform = currentButton.GetComponent<RectTransform>();
        if (position.y > 0) buttonRectTransform.position = transform.position + new Vector3(-0.1f, -0.6f, 0);
        else buttonRectTransform.position = transform.position + new Vector3(-0.1f, 0.6f, 0);
        RectTransform checkRectTransform = currentCheckButton.GetComponent<RectTransform>();
        if (position.y > 0) checkRectTransform.position = transform.position + new Vector3(0.5f, -0.6f, 0);
        else checkRectTransform.position = transform.position + new Vector3(0.5f, 0.6f, 0);
    }

    public void SetArrowButton()
    {
        RectTransform arrowRectTransform = currentArrowButton.GetComponent<RectTransform>();
        arrowRectTransform.rotation = transform.rotation;
        arrowRectTransform.position = transform.position - transform.up * 1.3f;
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
        ShipPanel.transform.Find("ShipImage").GetComponent<Image>().sprite = this.transform.Find("GameObject").GetComponent<SpriteRenderer>().sprite;
        ShipPanel.transform.Find("ShipImage").GetComponent<Image>().SetNativeSize();
        ShipPanel.transform.Find("StatText").GetComponent<TextMeshProUGUI>().text = currentHP + " / 3\n" + actionPoint + " / 2";
    }

    public void clickOff()
    {
        ShipBase[] foundObjects = FindObjectsOfType<ShipBase>();
        foreach (ShipBase obj in foundObjects)
        {
            foreach (Transform child in obj.transform)
            {
                if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
            }

            if (obj.clicked)
            {
                obj.clicked = false;
                obj.transform.rotation = obj.tempRotation;
                obj.ReAssignAttackDir();
                
                obj.lineRenderer.enabled = false;
                Destroy(obj.currentButton);
                Destroy(obj.currentCheckButton);
                Destroy(obj.currentArrowButton);
                obj.ShipPanel.SetActive(false);
            }
        }
    }

    public virtual void ReAssignAttackDir()
    {

    }

    public bool CheckSelected()
    {
        if (selected) return true;
        ShipBase[] foundObjects = FindObjectsOfType<ShipBase>();
        foreach (ShipBase obj in foundObjects)
        {
            if (obj.selected)
            {
                return false;
            }
        }

        return true;
    }
}
