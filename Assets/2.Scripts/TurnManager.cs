using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

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
    public static int usedAP;
    public event Action<int> WeightStart;

    // YJK, UI ���� ������Ʈ��
    public Button endTurnButton;
    public Button[] buttons;
    public TMPro.TextMeshProUGUI turnText, apText;

    public GameObject[] NavyVessel, PirateVessel;
    public GameObject VictoryPanel, ConfirmPanel, SurrenderPanel;
    public TMPro.TextMeshProUGUI VictoryText;
    public Sprite NavyImage, PirateImage;

    public float eachWeightTime = 5.0f;
    public static bool rangeOn = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnList.P1;
        usedAP = 0;
        GameManager.instance.ActionPoints = 1;
        ChangeTurnText(currentTurn);
        GameObject.Find("ShipPanel").SetActive(false);
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
        apText.text = "Island: " + GameManager.instance.ActionPoints.ToString();
    }

    public void EndTurnClicked()
    {
        if(GameManager.instance.ActionPoints > 0 || usedAP < 2)
        {
            ConfirmPanel.gameObject.SetActive(true);
            foreach(Button btn in buttons)
            {
                btn.enabled = false;
            }
        }
        else
        {
            usedAP = 0;
            currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
            if (GameObject.Find("ShipPanel") != null) GameObject.Find("ShipPanel").SetActive(false);
            GameManager.instance.ActionPoints = 1;
            ChangeTurnText(currentTurn);
            StartCoroutine("BoatTurn");
        }
    }

    // YJK, ���Ժ� �Լ����� eachWeightTime���� �����϶�� �̺�Ʈ ����
    IEnumerator BoatTurn()
    {
        for(int i = 1; i < 2; i++)
        {
            WeightStart?.Invoke(i);
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            //if(i > 1) OrderPanelList[i-2].GetComponent<Image>().color = Color.white;
            //OrderPanelList[i - 1].GetComponent<Image>().color = Color.green;
            yield return new WaitForSeconds(eachWeightTime);
        }
        //OrderPanelList[3].GetComponent<Image>().color = Color.white;
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        ChangeTurnText(currentTurn);
        yield return new WaitForSeconds(0.3f);
        if(NavyVessel[0] == null && NavyVessel[1] == null && NavyVessel[2] == null && NavyVessel[3] == null && NavyVessel[4] == null)
        {
            if(PirateVessel[0] == null && PirateVessel[1] == null && PirateVessel[2] == null && PirateVessel[3] == null && PirateVessel[4] == null)
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
                VictoryPanel.GetComponent<Image>().sprite = PirateImage;
                VictoryPanel.SetActive(true);
            }
        }
        else
        {
            if(PirateVessel[0] == null && PirateVessel[1] == null && PirateVessel[2] == null && PirateVessel[3] == null && PirateVessel[4] == null)
            {
                VictoryText.text = "Navy Victory!";
                VictoryText.color = Color.blue;
                VictoryPanel.GetComponent<Image>().sprite = NavyImage;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitPressed()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void ShowAttackRange()
    {
        if (rangeOn)
        {
            rangeOn = false;
            ShipBase[] foundObjects = FindObjectsOfType<ShipBase>();
            foreach (ShipBase obj in foundObjects)
            {
                foreach (Transform child in obj.transform)
                {
                    if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);                }
                obj.lineRenderer.enabled = false;
            }
        }
        else
        {
            rangeOn=true;
            ShipBase[] foundObjects = FindObjectsOfType<ShipBase>();
            foundObjects[0].clickOff();
            foreach (ShipBase obj in foundObjects)
            {
                obj.ShowAttackRange();
            }
        }
    }

    public void ConfirmYesPressed()
    {
        foreach (Button btn in buttons)
        {
            btn.enabled = true;
        }
        usedAP = 0;
        ConfirmPanel.SetActive(false);
        currentTurn = (TurnList)(((int)currentTurn + 1) % 4);
        if (GameObject.Find("ShipPanel") != null) GameObject.Find("ShipPanel").SetActive(false);
        GameManager.instance.ActionPoints = 1;
        ChangeTurnText(currentTurn);
        StartCoroutine("BoatTurn");
    }

    public void ConfirmNoPressed()
    {
        foreach (Button btn in buttons)
        {
            btn.enabled = true;
        }
        ConfirmPanel.SetActive(false);
    }

    public void SurrenderPressed()
    {
        foreach (Button btn in buttons)
        {
            btn.enabled = false;
        }
        SurrenderPanel.SetActive(true);
    }

    public void SurrenderYesPressed()
    {
        SurrenderPanel.SetActive(false);
        foreach (Button btn in buttons)
        {
            btn.enabled = false;
        }
        if (currentTurn == TurnList.P1)
        {
            VictoryText.text = "Pirates Victory!";
            VictoryText.color = Color.red;
            VictoryPanel.GetComponent<Image>().sprite = PirateImage;
            VictoryPanel.SetActive(true);
        }
        else
        {
            VictoryText.text = "Navy Victory!";
            VictoryText.color = Color.blue;
            VictoryPanel.GetComponent<Image>().sprite = NavyImage;
            VictoryPanel.SetActive(true);
        }
    }

    public void SurrenderNoPressed()
    {
        foreach (Button btn in buttons)
        {
            btn.enabled = true;
        }
        SurrenderPanel.SetActive(false);
    }
}
