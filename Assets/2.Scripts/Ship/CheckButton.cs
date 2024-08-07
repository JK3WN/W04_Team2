using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    public ShipBase ship;
    private GameManager gameManager;
    private Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        btn.onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        if(TurnManager.currentTurn == ship.team) gameManager.ActionPoints -= 1;
        else gameManager.ActionPoints-= 3;

        ship.clicked = false;
        ship.ResetAttackRange();
        foreach (Transform child in ship.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(ship.currentButton);
        Destroy(this.gameObject);
    }
}
