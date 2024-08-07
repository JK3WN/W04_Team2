using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 intPosition;
    public GameObject selectedShip;
    public RaycastHit2D hit;

    public LayerMask hitLayerMask;

    private bool hasClicked;

    void Update()
    {
        // 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭
        {
            // 화면의 클릭 위치를 월드 좌표로 변환
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x > -5 && mousePosition.x < 5 && mousePosition.y > -5 && mousePosition.y < 5){
                // 클릭 위치를 int형으로 변환 (소수점 이하 자르기)
                intPosition = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);
                hasClicked = true;
                // 클릭한 위치의 월드 좌표를 출력
                Debug.Log("Clicked Position: " + intPosition);

                hit = Physics2D.Raycast(intPosition, Vector2.zero, Mathf.Infinity, hitLayerMask);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Ship"))
                    {
                        selectedShip = hit.collider.gameObject;
                        selectedShip.GetComponent<ShipClickMove>().isSelected = true;
                    }
                }

                StartCoroutine(Zero());
            }
        }
    }

    IEnumerator Zero()
    {
        yield return new WaitForSeconds(0.5f);
        intPosition = new Vector2 (0, 0);
    }

    private void OnDrawGizmos()
    {
        if (hasClicked)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(intPosition, new Vector3(1, 1, 0));
        }
    }
}
