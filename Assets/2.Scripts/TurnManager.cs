using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // YJK, UI 관련 오브젝트들
    public Button endTurnButton;
    public TMPro.TextMeshProUGUI turnText, apText;
    public GameObject[] OrderPanelList;

    public float eachWeightTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnList.P1;
        GameManager.instance.ActionPoints = 10;
        ChangeTurnText(currentTurn);
        GameObject.Find("ShipPanel").SetActive(false);
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
        apText.text = "AP: " + GameManager.instance.ActionPoints.ToString();
    }

    public void EndTurnClicked()
    {
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        GameManager.instance.ActionPoints = 10;
        ChangeTurnText(currentTurn);
        StartCoroutine("BoatTurn");
    }

    // YJK, 무게별 함선들을 eachWeightTime마다 시작하라는 이벤트 보냄
    IEnumerator BoatTurn()
    {
        for(int i = 1; i <= 4; i++)
        {
            WeightStart?.Invoke(i);
            if(i > 1) OrderPanelList[i-2].GetComponent<Image>().color = Color.white;
            OrderPanelList[i - 1].GetComponent<Image>().color = Color.green;
            yield return new WaitForSeconds(eachWeightTime);
        }
        OrderPanelList[3].GetComponent<Image>().color = Color.white;
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        ChangeTurnText(currentTurn);
    }

    public void ChangeTurnText(TurnList turn)
    {
        switch (turn)
        {
            case TurnList.P1:
                turnText.text = "Player 1";
                turnText.color = Color.blue;
                apText.gameObject.SetActive(true);
                break;
            case TurnList.P2:
                turnText.text = "Player 2";
                turnText.color = Color.red;
                apText.gameObject.SetActive(true);
                break;
            default:
                turnText.text = "Wait";
                turnText.color = Color.magenta;
                apText.gameObject.SetActive(false);
                break;
        }
    }
}
