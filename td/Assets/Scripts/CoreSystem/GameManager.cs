using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Difficulty
{
    Easy,   // 40����, 100����
    Normal, // 50����, 90����
    Hard    // 60����, 80����
}

public class GameManager : MonoBehaviour
{
    // Delegate
    // Events

    // Singleton
    public static GameManager Instance { get; private set; }

    // Properties
    // Fields
    public int CurrentRound { get; private set; } // ���� ����
    public float Money { get; private set; } // ���� ��
    public Difficulty CurrentDifficulty { get; private set; } // ���� ���̵�

    [SerializeField] private Button easyButton;    // Easy ���̵� ��ư
    [SerializeField] private Button normalButton;  // Normal ���̵� ��ư
    [SerializeField] private Button hardButton;    // Hard ���̵� ��ư

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
        // �� ��ư�� Ŭ�� �̺�Ʈ �߰�
        easyButton.onClick.AddListener(() => StartGame(Difficulty.Easy));
        normalButton.onClick.AddListener(() => StartGame(Difficulty.Normal));
        hardButton.onClick.AddListener(() => StartGame(Difficulty.Hard));
    }

    // Event Handlers
    public void StartGame(Difficulty select)  // ���� ���� ��ư ���̵��� ������
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
