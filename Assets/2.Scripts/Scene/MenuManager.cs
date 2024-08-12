using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainPanel, HelpPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPressed()
    {
        SceneManager.LoadScene("Final");
    }

    public void HelpPressed()
    {
        MainPanel.SetActive(false);
        HelpPanel.SetActive(true);
    }

    public void QuitPressed()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void HomePressed()
    {
        HelpPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void PreviousPressed()
    {

    }

    public void NextPressed()
    {

    }
}
