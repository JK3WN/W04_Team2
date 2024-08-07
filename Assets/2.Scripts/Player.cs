using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 intPosition;

    void Update()
    {
        // ���콺 Ŭ�� ����
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
        {
            // ȭ���� Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ŭ�� ��ġ�� int������ ��ȯ (�Ҽ��� ���� �ڸ���)
            intPosition = new Vector2(Mathf.FloorToInt(mousePosition.x) + 0.5f, Mathf.FloorToInt(mousePosition.y) + 0.5f);


            // Ŭ���� ��ġ�� ���� ��ǥ�� ���
            Debug.Log("Clicked Position: " + intPosition);

            StartCoroutine(Zero());
        }
    }

    IEnumerator Zero()
    {
        yield return new WaitForSeconds(0.5f);
        intPosition = new Vector2 (0, 0);
    }
}
