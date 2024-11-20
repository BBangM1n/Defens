using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // Delegate
    // Events

    // Definitions
    // Properties
    // Fields
    [SerializeField] UIManager uiManager;

    private bool isBossRound = false;
    private bool bossDefeated = false;
    private int unitCount = 0;
    private int currentStage = 1;

    private float timeRemaining = 45f;
    private Coroutine currentCoroutine = null;

    private GameManager gameManager = GameManager.Instance;

    public int MaxUnitCount = 100; // easy : 100, normal : 90, hard : 80
    public int MaxStage = 40; // eash : 40, normal : 50, hard : 60


    // Unity Messages
    void Start()
    {
        //init();
    }

    // Methods
    public void DefeatBoss()
    {
        if (isBossRound)
        {
            bossDefeated = true;
            Debug.Log("������ óġ�Ͽ����ϴ�!");
        }
    }
    // Functions
    private void init()
    {
        currentCoroutine = StartCoroutine(timerCoroutine());

        if(gameManager.CurrentDifficulty == Difficulty.Easy)
        {
            MaxStage = 40;
            MaxUnitCount = 100;
        }
        else if(gameManager.CurrentDifficulty == Difficulty.Normal)
        {
            MaxStage = 50;
            MaxUnitCount = 90;
        }
        else
        {
            MaxStage = 60;
            MaxUnitCount = 80;
        }
        // �ƽ� ����ī��Ʈ �޾ƿ���
        // �ƽ� �������� �޾ƿ���
    }
    private void startNewStage()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        if (currentStage >= MaxStage)
        {
            GameClear();
            return;
        }

        timeRemaining = 45f;

        currentStage += 1;
        isBossRound = (currentStage % 10 == 0); // 10�� ��� ���������� ���� ����

        uiManager.SetStageText(currentStage);
        uiManager.SetUnitCountText(unitCount, MaxUnitCount);

        bossDefeated = false; // ���ο� �������� ���� �� ���� óġ ���� �ʱ�ȭ

        if (isBossRound)
        {
            SpawnBoss(); // TODO : ������ƮǮ������ ��ȯ
        }

        currentCoroutine = StartCoroutine(timerCoroutine());
    }

    private void SpawnBoss()
    {
        Debug.Log("���� ����");
    }
    private void GameClear()
    {
        Debug.Log("���� Ŭ����");
    }

    // Event Handlers
    private void spawnUnitHandler()
    {
        if (unitCount >= MaxUnitCount)
        {
            StopCoroutine(currentCoroutine);
        }
        else
        {
            unitCount++;
            uiManager.SetUnitCountText(unitCount, MaxUnitCount);
        }
    }

    // Unity Coroutine
    private IEnumerator timerCoroutine()
    {
        uiManager.SetTimerText(timeRemaining);

        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);

            if (!isBossRound && timeRemaining > 5)
            {
                spawnUnitHandler(); // �Ϲ� ������ ��� ���� ��ȯ
            }
      
            timeRemaining--;
            uiManager.SetTimerText(timeRemaining);

            if (isBossRound && bossDefeated) // ���� ���忡�� ������ óġ�� ���
            {
                break; // �ڷ�ƾ �����ϰ� ���� ����� �Ѿ
            }
        }

        // Ÿ�̸� ���� �� ó��
        uiManager.SetTimerText(0);

        if (isBossRound && !bossDefeated)
        {
            Debug.Log("������ óġ���� ���߽��ϴ�!");// ���� ���忡�� ������ ���� ���ϸ� �й� ó��
        }
        else
        {
            yield return new WaitForSeconds(1f);
            startNewStage();
        }
    }
}
