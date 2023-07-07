using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int id;
    public List<SideQuest> sideQuests;
    public event Action OnGamePassed;


    private void Awake()
    {
        InitializeQuest();
    }

    private void Start()
    {
        InitializeSideQuest();
    }

    private void InitializeQuest()
    {
        sideQuests.ForEach(q => q.OnQuestPassed += InitializeSideQuest);
    }

    private void InitializeSideQuest()
    {
        sideQuests.ForEach(q => q.QuestIsActive = false);
        var gameIsPassed = !sideQuests.Any(q => q.questIsPassed != true);
        if (gameIsPassed)
        {
            OnGamePassed?.Invoke();
            Debug.Log("GAME IS PASSED");
            return;
        }
        var quest = sideQuests.FirstOrDefault(q => !q.questIsPassed && !q.QuestIsActive).QuestIsActive = true;
    }
}
