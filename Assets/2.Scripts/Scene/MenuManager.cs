using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainPanel, HelpPanel;
    public TMPro.TextMeshProUGUI pageNumText;
    public GameObject helpImage;
    public string helpPath = "Assets/4.Images/Help";
    public List<Sprite> helpList;
    private int currentPage, maxPage;

    // Start is called before the first frame update
    void Start()
    {
        /*
        helpList = new List<Sprite>();
        string[] files = Directory.GetFiles(helpPath, "*.png");
        foreach (string file in files)
        {
            byte[] fileData = File.ReadAllBytes(file);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            helpList.Add(sprite);
        }
        */
        maxPage = helpList.Count;
        currentPage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        pageNumText.text = currentPage + " / " + maxPage;
        helpImage.GetComponent<Image>().sprite = helpList[currentPage - 1];
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
        if(currentPage > 1) currentPage--;
    }

    public void NextPressed()
    {
        if(currentPage < maxPage) currentPage++;
    }
}
