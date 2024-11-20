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
            Debug.Log("보스를 처치하였습니다!");
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
        // 맥스 유닛카운트 받아오기
        // 맥스 스테이지 받아오기
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
        isBossRound = (currentStage % 10 == 0); // 10의 배수 스테이지는 보스 라운드

        uiManager.SetStageText(currentStage);
        uiManager.SetUnitCountText(unitCount, MaxUnitCount);

        bossDefeated = false; // 새로운 스테이지 시작 시 보스 처치 상태 초기화

        if (isBossRound)
        {
            SpawnBoss(); // TODO : 오브젝트풀링으로 소환
        }

        currentCoroutine = StartCoroutine(timerCoroutine());
    }

    private void SpawnBoss()
    {
        Debug.Log("보스 출현");
    }
    private void GameClear()
    {
        Debug.Log("게임 클리어");
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
                spawnUnitHandler(); // 일반 라운드의 경우 유닛 소환
            }
      
            timeRemaining--;
            uiManager.SetTimerText(timeRemaining);

            if (isBossRound && bossDefeated) // 보스 라운드에서 보스를 처치한 경우
            {
                break; // 코루틴 종료하고 다음 라운드로 넘어감
            }
        }

        // 타이머 종료 시 처리
        uiManager.SetTimerText(0);

        if (isBossRound && !bossDefeated)
        {
            Debug.Log("보스를 처치하지 못했습니다!");// 보스 라운드에서 보스를 잡지 못하면 패배 처리
        }
        else
        {
            yield return new WaitForSeconds(1f);
            startNewStage();
        }
    }
}
