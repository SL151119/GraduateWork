using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem;
using Gameplay.FactorySystem.Configurations;
using Gameplay.FactorySystem.Items;
using Gameplay.FactorySystem.Storage;
using Gameplay.PlayerBehaviour;
using UnityEngine;

namespace Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<StorageBase> storages;
        [SerializeField] private List<FactoryBase> factories;
        [SerializeField] private List<QuestSpot> questSpots;
        [SerializeField] private ShopBase shop;
        [SerializeField] private WalletController walletController;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private ItemsConfigsHolder itemsConfigsHolder;

        private Coroutine _triggerActionRoutine;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            //shop.OnPlayerEnter += StartShopTriggerActionTracking;
            //shop.OnPlayerExit += StopTriggerActionTracking;


            questSpots.ForEach(q =>
            {
                q.OnPlayerEnter += StartQuestTriggerActionTracking;
                q.OnPlayerExit += StopTriggerActionTracking;
            });

            storages.ForEach(s =>
            {
                s.OnPlayerEnter += StartTriggerActionTracking;
                s.OnPlayerExit += StopTriggerActionTracking;
            });

            walletController.IncreaseBalance(10000000);
        }

        private void StartTriggerActionTracking(StorageBase storage, List<int> itemsIds, int value)
        {
            _triggerActionRoutine = StartCoroutine(TriggerActionCoroutine(storage, itemsIds, value));
        }

        //private void StartShopTriggerActionTracking(ShopBase shopBase, int value)
        //{
        //    _triggerActionRoutine = StartCoroutine(ShopTriggerActionCoroutine(shopBase, value));
        //}

        private void StartQuestTriggerActionTracking(SideQuest sideQuest)
        {
            _triggerActionRoutine = StartCoroutine(QuestTriggerActionCoroutine(sideQuest));
        }

        private void StopTriggerActionTracking()
        {
            StopCoroutine(_triggerActionRoutine);
            _triggerActionRoutine = null;
        }

        //private IEnumerator ShopTriggerActionCoroutine(ShopBase shopBase, int value)
        //{
        //    while (true)
        //    {
        //        yield return new WaitForSeconds(playerController.GrabDuration);
        //        SellTriggerAction(shopBase, value);
        //    }
        //}

        private IEnumerator QuestTriggerActionCoroutine(SideQuest sideQuest)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                QuestTriggerAction(sideQuest);
            }
        }

        private IEnumerator TriggerActionCoroutine(StorageBase storage, List<int> itemsIds, int value)
        {
            while (true)
            {
                yield return new WaitForSeconds(playerController.GrabDuration);
                TriggerAction(storage, itemsIds, value);
            }
        }

        //private void SellTriggerAction(ShopBase shopBase, int value)
        //{
        //    walletController.GetExistingItemsIds().ForEach(itemId =>
        //    {
        //        if (walletController.HaveNoItems(itemId))
        //        {
        //            return;
        //        }
        //        walletController.IncreaseBalance(itemsConfigsHolder.itemsConfigs.Find(i => i.id == itemId).price);
        //        walletController.DecreaseWallet(itemId, value);
        //        //if (!playerController.BackPackView.ItemByIdIsExist(itemId))
        //        //{
        //        //    return;
        //        //}
        //        //var item = playerController.BackPackView.GetItemById(itemId);
        //        //itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(item, playerController.BackPackView.RemoveItem(itemId), shop.transform, true);
        //    });
        //}

        private void QuestTriggerAction(SideQuest sideQuest)
        {
            if (sideQuest.questIsPassed)
            {
                return;
            }

            walletController.GetExistingItemsIds().ForEach(itemId =>
            {
                if (walletController.HaveNoItems(itemId))
                {
                    return;
                }
                if(sideQuest.ItemForQuest.id == itemId)
                {
                    walletController.DecreaseWallet(itemId, 1);
                    sideQuest.CurrentAmount = 1;
                }
                //if (!playerController.BackPackView.ItemByIdIsExist(itemId))
                //{
                //    return;
                //}
                //var item = playerController.BackPackView.GetItemById(itemId);
                //itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(item, playerController.BackPackView.RemoveItem(itemId), shop.transform, true);
            });
        }


        private void TriggerAction(StorageBase storage, List<int> itemsIds, int value)
        {
            itemsIds.ForEach(itemId =>
            {
                switch (storage.StorageTypes)
                {
                    case StorageTypes.Input:
                    {
                        if (walletController.HaveNoItems(itemId) || storage.FullOfItemsById(itemId))
                        {
                            return;
                        }
                        walletController.DecreaseWallet(itemId, value);
                        storage.FillStorage(itemId, value);
                        //if (!playerController.BackPackView.ItemByIdIsExist(itemId))
                        //{
                        //    return;
                        //}
                        //var item = playerController.BackPackView.GetItemById(itemId);
                        //itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(item,playerController.BackPackView.RemoveItem(itemId),storage.transform, true);
                        break;
                    }
                    case StorageTypes.Output:
                    {
                        if (walletController.StackIsFull(itemId) || storage.StorageIsEmpty())
                        {
                            return;
                        }
                    
                        walletController.IncreaseWallet(itemId, value);
                        storage.GrabFromStorage(itemId, value);
                        //if (!playerController.BackPackView.AllSlotsAreBusy())
                        //{
                        //    var item = itemsPool.GetFreeItemById(itemId);
                        //    itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(item, storage.transform,
                        //    playerController.BackPackView.AddItem(item),false);
                        //}

                        break;
                    }
                }
            });
        }
    }
}
