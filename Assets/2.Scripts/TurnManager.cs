using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject[] NavyVessel, PirateVessel;
    public GameObject VictoryPanel;
    public TMPro.TextMeshProUGUI VictoryText;

    public float eachWeightTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnList.P1;
        GameManager.instance.ActionPoints = 1;
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
        apText.text = "Ground: " + GameManager.instance.ActionPoints.ToString();
    }

    public void EndTurnClicked()
    {
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        if (GameObject.Find("ShipPanel") != null) GameObject.Find("ShipPanel").SetActive(false);
        GameManager.instance.ActionPoints = 1;
        ChangeTurnText(currentTurn);
        StartCoroutine("BoatTurn");
    }

    // YJK, 무게별 함선들을 eachWeightTime마다 시작하라는 이벤트 보냄
    IEnumerator BoatTurn()
    {
        for(int i = 1; i < 2; i++)
        {
            WeightStart?.Invoke(i);
            //if(i > 1) OrderPanelList[i-2].GetComponent<Image>().color = Color.white;
            //OrderPanelList[i - 1].GetComponent<Image>().color = Color.green;
            yield return new WaitForSeconds(eachWeightTime);
        }
        //OrderPanelList[3].GetComponent<Image>().color = Color.white;
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        ChangeTurnText(currentTurn);
        yield return new WaitForSeconds(0.3f);
        if(NavyVessel[0] == null && NavyVessel[1] == null && NavyVessel[2] == null && NavyVessel[3] == null)
        {
            if(PirateVessel[0] == null && PirateVessel[1] == null && PirateVessel[2] == null && PirateVessel[3] == null)
            {
                VictoryText.text = "Draw!";
                VictoryText.color = Color.black;
                VictoryPanel.GetComponent<Image>().sprite = null;
                VictoryPanel.SetActive(true);
            }
            else
            {
                VictoryText.text = "Pirates Victory!";
                VictoryText.color = Color.red;
                VictoryPanel.SetActive(true);
            }
        }
        else
        {
            if(PirateVessel[0] == null && PirateVessel[1] == null && PirateVessel[2] == null && PirateVessel[3] == null)
            {
                VictoryText.text = "Navy Victory!";
                VictoryText.color = Color.blue;
                VictoryPanel.SetActive(true);
            }
        }
    }

    public void ChangeTurnText(TurnList turn)
    {
        switch (turn)
        {
            case TurnList.P1:
                turnText.text = "Navy";
                turnText.color = Color.blue;
                apText.gameObject.SetActive(true);
                break;
            case TurnList.P2:
                turnText.text = "Pirates";
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

    public void RestartPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitPressed()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
}
