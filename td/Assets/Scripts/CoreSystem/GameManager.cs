using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Difficulty
{
    Easy,   // 40라운드, 100마리
    Normal, // 50라운드, 90마리
    Hard    // 60라운드, 80마리
}

public class GameManager : MonoBehaviour
{
    // Delegate
    // Events

    // Singleton
    public static GameManager Instance { get; private set; }

    // Properties
    // Fields
    public int CurrentRound { get; private set; } // 현재 라운드
    public float Money { get; private set; } // 가진 돈
    public Difficulty CurrentDifficulty { get; private set; } // 현재 난이도

    [SerializeField] private Button easyButton;    // Easy 난이도 버튼
    [SerializeField] private Button normalButton;  // Normal 난이도 버튼
    [SerializeField] private Button hardButton;    // Hard 난이도 버튼

    // Unity Messages
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        init();
    }

    // Method
    // Functions
    private void init()
    {
        // 각 버튼에 클릭 이벤트 추가
        easyButton.onClick.AddListener(() => StartGame(Difficulty.Easy));
        normalButton.onClick.AddListener(() => StartGame(Difficulty.Normal));
        hardButton.onClick.AddListener(() => StartGame(Difficulty.Hard));
    }

    // Event Handlers
    public void StartGame(Difficulty select)  // 게임 시작 버튼 난이도를 지정함
    {
        CurrentDifficulty = select;
        SceneManager.LoadScene("GameScene");
    }
    public void PauseGame() 
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void EndGame() 
    {

    }

    // Unity Coroutine
    // Interface
}
