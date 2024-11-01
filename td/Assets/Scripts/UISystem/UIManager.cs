using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Delegate
    // Events

    // Definitions
    // Properties
    // Fields
    [SerializeField] private Text textStage;
    [SerializeField] private Text textTimer;
    [SerializeField] private Text textSpeed;

    private int speedLevel = 0;

    // Unity Messages
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    // Methods
    public void SetTimer(float time)
    {
        // �ð��� �� ���
        int minutes = (int)(time / 60); // ��ü �� ��
        int seconds = (int)(time % 60); // ���� �� ��

        // "00:00" �������� ǥ��
        textTimer.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
    public void SetStage(int stage)
    {
        textStage.text = stage.ToString() + " Stage";
    }


    // Functions
    // Event Handlers
    public void SetGameSpeed()
    {
        if (speedLevel == 0)
        {
            speedLevel = 1;
            Time.timeScale = 1.5f;
            textSpeed.text = "1.5";
        }
        else if (speedLevel == 1)
        {
            speedLevel = 2;
            Time.timeScale = 2;
            textSpeed.text = "2";
        }
        else if (speedLevel == 2)
        {
            speedLevel = 0;
            Time.timeScale = 1;
            textSpeed.text = "1";
        }

        Debug.Log(Time.timeScale);
    }
    // Unity Coroutine
    // Interface
}
