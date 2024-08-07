using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Button startButton;
    public Button tutorialButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ���
        startButton.onClick.AddListener(OnStartButtonClicked);
        tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked");
        // ���� ���� ���� (��: ���� ������ ��ȯ)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    void OnTutorialButtonClicked()
    {
        Debug.Log("Tutorial Button Clicked");
        // Ʃ�丮�� ���� ���� (��: Ʃ�丮�� ������ ��ȯ)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene");
    }

    void OnQuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
        // ���� ���� ����
        Application.Quit();
    }
}
