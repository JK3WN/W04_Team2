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
            ship.selected = true;
            ship.actionPoint -= 1;
        }

        ship.tempRotation = ship.transform.rotation;
        ship.clickOff();
        Destroy(this.gameObject);
    }
}
