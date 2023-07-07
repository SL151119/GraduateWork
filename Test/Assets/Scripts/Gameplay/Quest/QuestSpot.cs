using Gameplay.FactorySystem;
using Gameplay.FactorySystem.Configurations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestSpot : MonoBehaviour
{
    [SerializeField] private string textTemplate;
    [SerializeField] private SideQuest sideQuest;
    [SerializeField] private FactoryView spotView;
    [SerializeField] private ItemsConfigsHolder itemsConfigs;
    [SerializeField] private GameObject ui;

    public event Action<SideQuest> OnPlayerEnter;
    public event Action OnPlayerExit;

    private void Awake()
    {
        sideQuest.OnQuestActive += InitializeSpot;
        sideQuest.OnQuestPassed += DeinitializeSpot;
        sideQuest.OnQuestUpdate += UpdateView;
    }

    private void InitializeSpot()
    {
        ui.SetActive(true);
        var icon = itemsConfigs.itemsConfigs.Find(x => x.id == sideQuest.ItemForQuest.id).sprite;
        spotView.gameObject.SetActive(true);
        spotView.SetText(string.Format(textTemplate, sideQuest.CurrentAmount, sideQuest.NeededAmound));
        spotView.SetSprite(icon);
    }

    private void UpdateView()
    {
        spotView.SetText(string.Format(textTemplate, sideQuest.CurrentAmount, sideQuest.NeededAmound));
    }

    private void DeinitializeSpot()
    {
        spotView.gameObject.SetActive(false);
        ui.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke(sideQuest);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit?.Invoke();
        }
    }
}
