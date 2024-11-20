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
    [SerializeField] private Button[] buttons; // 0 : ����, 1 : �Ǹ�, 2 : ��ġ, 3 : ����, 4 : â��, 5 : ���丮 & ���潺, 6 : ����, 7 : ��ȭ
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
        Debug.Log("Create ��� ����");
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
            storyButtonText.text = "���潺";
        }
        else
        {
            fieldType = FieldType.Defence;
            screenManager.MoveToDefence();
            storyButtonText.text = "���丮";
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
