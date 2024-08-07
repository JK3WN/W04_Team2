using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
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
        // 이동 코드
    }
}
