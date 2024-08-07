using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    public ShipBase ship;
    private Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        if(TurnManager.currentTurn == ship.team) GameManager.instance.ActionPoints -= 1;
        else GameManager.instance.ActionPoints-= 3;

        ship.clicked = false;
        ship.ResetAttackRange();
        foreach (Transform child in ship.transform)
        {
            if (!child.gameObject.CompareTag("Pos")) Destroy(child.gameObject);
        }
        Destroy(ship.currentButton);
        Destroy(ship.currentArrowButton);
        ship.ShipPanel.SetActive(false);
        Destroy(this.gameObject);
    }
}
