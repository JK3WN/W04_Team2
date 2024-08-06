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

    public Button endTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnList.P1;
        //StartCoroutine("temp");
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
    }

    IEnumerator temp()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            EndTurnClicked();
            Debug.Log(currentTurn);
        }
    }
}
