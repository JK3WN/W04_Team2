using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceIsland : MonoBehaviour
{
    public GameObject island, islandPreview;

    public Vector2 intPos;
    public bool islandReady = false;

    public RaycastHit2D hit;

    // Update is called once per frame
    void Update()
    {
        // YJK, 섬 놓을 준비 하는 버튼 누르면 islandReady를 참으로 만들어 다음 내용 진행
        if (islandReady)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            intPos = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);
            hit = Physics2D.Raycast(intPos, Vector2.zero, Mathf.Infinity);
            Debug.Log(hit.collider);
            if (mousePosition.x > -5 && mousePosition.x < 5 && mousePosition.y > -5 && mousePosition.y < 5 && (hit.collider == null || (hit.collider != null && !hit.collider.CompareTag("Ship") && !hit.collider.CompareTag("Land"))))
            {
                islandPreview.SetActive(true);
                islandPreview.transform.SetPositionAndRotation(intPos, Quaternion.identity);
                if (Input.GetMouseButtonDown(0))
                {
                    islandReady = false;
                    GameManager.instance.ActionPoints--;
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
            islandPreview.SetActive(false);
        }
    }
}
