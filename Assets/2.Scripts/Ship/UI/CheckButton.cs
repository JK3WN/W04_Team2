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
        if(ship.tempRotation != ship.transform.rotation)
        {
            ship.actionPoint -= 1;
        }

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
