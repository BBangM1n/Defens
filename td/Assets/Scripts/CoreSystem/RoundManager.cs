using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // Delegate
    // Events

    // Definitions
    // Properties
    // Fields
    [SerializeField] UIManager uiManager;

    private float timeRemaining = 45f;
    private int currentstage = 0;

    // Unity Messages
    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    // Methods
    // Functions
    // Event Handlers

    // Unity Coroutine
    private IEnumerator TimerCoroutine()
    {
        uiManager.SetTimer(timeRemaining);

        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f); // 1초 대기
            timeRemaining--; // 시간 감소
            uiManager.SetTimer(timeRemaining); // UI 업데이트
        }


        currentstage += 1;

        // 타이머 종료 시 처리
        uiManager.SetTimer(0); // 타이머 종료 시 "00:00" 표시
    }
    // Interface
}
