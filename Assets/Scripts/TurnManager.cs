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

    public Button endTurnButton;
    public float eachWeightTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnList.P1;
    }

    // Update is called once per frame
    void Update()
    {
        // YJK, currentTurn이 P1이나 P2일 때만 endTurnButton 활성화
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
        StartCoroutine("BoatTurn");
    }

    // YJK, 무게별 함선들을 eachWeightTime마다 시작하라는 이벤트 보냄
    IEnumerator BoatTurn()
    {
        for(int i = 1; i <= 4; i++)
        {
            WeightStart?.Invoke(i);
            yield return new WaitForSeconds(eachWeightTime);
        }
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
    }
}
