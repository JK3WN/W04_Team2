using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 intPosition;
    public GameObject selectedShip;
    public RaycastHit2D hit;

    public LayerMask hitLayerMask;

    void Update()
    {
        // ���콺 Ŭ�� ����
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
        {
            // ȭ���� Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ŭ�� ��ġ�� int������ ��ȯ (�Ҽ��� ���� �ڸ���)
            intPosition = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);

            hit = Physics2D.Raycast(intPosition, Vector2.zero, Mathf.Infinity, hitLayerMask);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Ship"))
                {
                    selectedShip = hit.collider.gameObject;
                    selectedShip.GetComponent<ShipClickMove>().isSelected = true;
                }
            }

            // Ŭ���� ��ġ�� ���� ��ǥ�� ���
            //Debug.Log("Clicked Position: " + intPosition);

            StartCoroutine(Zero());
        }
    }

    IEnumerator Zero()
    {
        yield return new WaitForSeconds(0.5f);
        intPosition = new Vector2 (0, 0);
    }
}
