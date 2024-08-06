using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TurnList
{
    P1 = 0,
    AfterP1 = 1,
    P2 = 2,
    AfterP2 = 3
}

public class TurnManager : MonoBehaviour
{
    public static TurnList currentTurn = TurnList.P1;
    public event Action<int> WeightStart;

    // YJK, UI ���� ������Ʈ��
    public Button endTurnButton;
    public TMPro.TextMeshProUGUI turnText;

    public float eachWeightTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnList.P1;
        ChangeTurnText(currentTurn);
    }

    // Update is called once per frame
    void Update()
    {
        // YJK, currentTurn�� P1�̳� P2�� ���� endTurnButton Ȱ��ȭ
        if((int)currentTurn % 2 == 0)
        {
            endTurnButton.gameObject.SetActive(true);
        }
        else
        {
            endTurnButton.gameObject.SetActive(false);
        }
    }

    public void EndTurnClicked()
    {
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        ChangeTurnText(currentTurn);
        StartCoroutine("BoatTurn");
    }

    // YJK, ���Ժ� �Լ����� eachWeightTime���� �����϶�� �̺�Ʈ ����
    IEnumerator BoatTurn()
    {
        for(int i = 1; i <= 4; i++)
        {
            WeightStart?.Invoke(i);
            yield return new WaitForSeconds(eachWeightTime);
        }
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        ChangeTurnText(currentTurn);
    }

    public void ChangeTurnText(TurnList turn)
    {
        switch (turn)
        {
            case TurnList.P1:
                turnText.text = "Player 1";
                break;
            case TurnList.P2:
                turnText.text = "Player 2";
                break;
            default:
                turnText.text = "Wait";
                break;
        }
    }
}
