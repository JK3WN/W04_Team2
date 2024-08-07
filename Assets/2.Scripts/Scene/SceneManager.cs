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
        // 버튼 클릭 이벤트에 메서드 등록
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
        // 게임 시작 로직 (예: 게임 씬으로 전환)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    void OnTutorialButtonClicked()
    {
        Debug.Log("Tutorial Button Clicked");
        // 튜토리얼 시작 로직 (예: 튜토리얼 씬으로 전환)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene");
    }

    void OnQuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
        // 게임 종료 로직
        Application.Quit();
    }
}
