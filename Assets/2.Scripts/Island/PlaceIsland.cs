using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceIsland : MonoBehaviour
{
    public GameObject island, islandPreview;
    public Button islandButton;
    public TMPro.TextMeshProUGUI islandText;

    public Vector2 intPos;
    public bool islandReady = false;

    public RaycastHit2D hit;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.ActionPoints <= 0 || (int)TurnManager.currentTurn % 2 == 1) islandButton.enabled = false;
        else islandButton.enabled = true;
        // YJK, 섬 놓을 준비 하는 버튼 누르면 islandReady를 참으로 만들어 다음 내용 진행
        if (islandReady)
        {
            islandText.text = "Cancel";
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

                    Destroy(obj.currentButton);
                    Destroy(obj.currentCheckButton);
                    Destroy(obj.currentArrowButton);
                    Destroy(obj.currentAP);
                    obj.ShipPanel.SetActive(false);
                }
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            intPos = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);
            hit = Physics2D.Raycast(intPos, Vector2.zero, Mathf.Infinity);
            if (mousePosition.x > -5 && mousePosition.x < 5 && mousePosition.y > -5 && mousePosition.y < 5 && (hit.collider == null || (hit.collider != null && !hit.collider.CompareTag("Ship") && !hit.collider.CompareTag("Land") && !hit.collider.CompareTag("Dirt"))))
            {
                islandPreview.SetActive(true);
                islandPreview.transform.SetPositionAndRotation(intPos, Quaternion.identity);
                if (Input.GetMouseButtonDown(0))
                {
                    islandReady = false;
                    GameManager.instance.ActionPoints--;
                    RaycastHit2D[] hits = Physics2D.RaycastAll(intPos, Vector2.zero, Mathf.Infinity);
                    foreach(RaycastHit2D hitt in hits)
                    {
                        if (hitt.collider.CompareTag("Dirt")) Destroy(hitt.collider.gameObject);
                    }
                    GameObject islandInstance = GameObject.Instantiate(island, intPos, Quaternion.identity);
                }
            }
            else
            {
                islandPreview.SetActive(false);
            }
        }
        else
        {
            islandText.text = "Place Island";
            islandPreview.SetActive(false);
        }
    }

    public void PlaceIslandClicked()
    {
        islandReady = !islandReady;
    }
}
