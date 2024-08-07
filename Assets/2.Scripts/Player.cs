using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        // ���콺 Ŭ�� ����
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
        {
            // ȭ���� Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ŭ�� ��ġ�� int������ ��ȯ (�Ҽ��� ���� �ڸ���)
            Vector2 intPosition = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);

            // �Ǵ� �ݿø��� ���
            // Vector2Int intPosition = new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));


            // Ŭ���� ��ġ�� ���� ��ǥ�� ���
            Debug.Log("Clicked Position: " + intPosition);
        }
    }
}
