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
            yield return new WaitForSeconds(1f); // 1�� ���
            timeRemaining--; // �ð� ����
            uiManager.SetTimer(timeRemaining); // UI ������Ʈ
        }


        currentstage += 1;

        // Ÿ�̸� ���� �� ó��
        uiManager.SetTimer(0); // Ÿ�̸� ���� �� "00:00" ǥ��
    }
    // Interface
}
