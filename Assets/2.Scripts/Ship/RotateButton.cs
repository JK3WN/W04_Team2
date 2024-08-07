using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
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
        ship.Rotate();
    }
}
