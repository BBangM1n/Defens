using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FieldType
{
    Defence,
    Story
}


public class ButtonManager : MonoBehaviour
{
    private const int RequiredButtonCount = 8;

    // Delegate
    // Events

    // Definitions
    // Properties
    // Fields
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private Button[] buttons; // 0 : 생성, 1 : 판매, 2 : 배치, 3 : 조합, 4 : 창고, 5 : 스토리 & 디펜스, 6 : 도박, 7 : 강화
    private Text storyButtonText;
    private FieldType fieldType;



    private void Start()
    {
        init();
    }

    private void init()
    {
        if(buttons == null || buttons.Length < RequiredButtonCount)
        {
            buttons = GetComponentsInChildren<Button>();
        }

        storyButtonText = buttons[5].GetComponentInChildren<Text>();
    }

    // Event Handlers
    public void CreateButtonClick()
    {
        Debug.Log("Create 기능 실행");
    }
    public void SellButtonClick()
    {

    }
    public void AssignButtonClick() { }
    public void CombinationButtonClick() { }
    public void StorageButtonClick() { }
    public void StoryButtonClick() 
    {
        if(fieldType == FieldType.Defence)
        {
            fieldType = FieldType.Story;
            screenManager.MoveToStory();
            storyButtonText.text = "디펜스";
        }
        else
        {
            fieldType = FieldType.Defence;
            screenManager.MoveToDefence();
            storyButtonText.text = "스토리";
        }
    }
    public void GamblingButtonClick() { }
    public void Enhance() { }

    // Methods
    // Functions
    private void create() 
    {

    }

    // Unity Coroutine
    // Interface
}
