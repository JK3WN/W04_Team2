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
        // ���콺 Ŭ�� ����
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
        {
            // ȭ���� Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x > -5 && mousePosition.x < 5 && mousePosition.y > -5 && mousePosition.y < 5){
                // Ŭ�� ��ġ�� int������ ��ȯ (�Ҽ��� ���� �ڸ���)
                intPosition = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);
                hasClicked = true;
                // Ŭ���� ��ġ�� ���� ��ǥ�� ���
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
