using Gameplay.FactorySystem.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuest : MonoBehaviour
{
    public int id;
    public bool questIsPassed;

    public event Action OnQuestPassed;
    public event Action OnQuestActive;
    public event Action OnQuestUpdate;


    public int NeededAmound
    {
        get => neededAmountOfItems;
    }

    public bool QuestIsActive
    {
        get => _questIsActive;
        set
        {
            if (value)
            {
                _questIsActive = value;
                OnQuestActive?.Invoke();
                return;
            }

            _questIsActive = value;

        }
    }


    public int CurrentAmount
    {
        get => _currentAmount;
        set
        {
            if(_currentAmount + value >= neededAmountOfItems)
            {
                questIsPassed = true;
                OnQuestPassed?.Invoke();
                return;
            }

            _currentAmount += value;
            OnQuestUpdate?.Invoke();

        }
    }

    public Item ItemForQuest
    {
        get => itemForQuest;
    }


    [SerializeField] private Item itemForQuest;
    [SerializeField] private int neededAmountOfItems;

    private int _currentAmount;
    private bool _questIsActive;

}
