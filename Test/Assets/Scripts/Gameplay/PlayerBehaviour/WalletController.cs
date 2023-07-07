using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem.Configurations;
using UnityEngine;

namespace Gameplay.PlayerBehaviour
{
    public class WalletController : MonoBehaviour
    {
        [SerializeField] private ItemsConfigsHolder itemsConfigsHolder;
        [SerializeField] private List<WalletView> walletViews;
        [SerializeField] private WalletView balanceWalletView;
        [SerializeField] private WalletEntity walletEntity;
        [SerializeField] private Sprite moneyIcon;

        public int Balance
        {
            get => walletEntity.moneyBalance;
        }

        private void Start()
        {
            InitializeWallet();
        }

        private void InitializeWallet()
        {
            balanceWalletView.SetIcon(moneyIcon);
            balanceWalletView.SetText(walletEntity.moneyBalance);
            balanceWalletView.Activate();
            walletEntity.items.ForEach(i =>
            {
                var currentView = walletViews.Find(v => !v.IsActive());
                var currentConfig = itemsConfigsHolder.itemsConfigs.Find(c => c.id == i.itemId);
                currentView.SetIcon(currentConfig.sprite);
                currentView.SetText(i.itemsCapacity);
                currentView.Id = i.itemId;
                currentView.Activate();
            });
        }


        public int GetTotalCapacity(int id)
        {
            var currentItem = walletEntity.items.Find(i => i.itemId == id);
            return currentItem.itemsCapacity;
        }

        public void IncreaseBalance(int value)
        {
            walletEntity.moneyBalance += value;
            balanceWalletView.SetText(walletEntity.moneyBalance);
        }

        public void DecreaseBalance(int value)
        {
            if (walletEntity.moneyBalance - value < 0)
            {
                return;
            }

            walletEntity.moneyBalance -= value;
            balanceWalletView.SetText(walletEntity.moneyBalance);
        }

        public void IncreaseWallet(int id, int value)
        {
            var currentItem = walletEntity.items.Find(i => i.itemId == id);
            currentItem.itemsCapacity += value;
            walletViews.Find(v => v.Id == id).SetText(currentItem.itemsCapacity);
        }

        public void DecreaseWallet(int id, int value)
        {
            var currentItem = walletEntity.items.Find(i => i.itemId == id);
            currentItem.itemsCapacity -= value;
            walletViews.Find(v => v.Id == id).SetText(currentItem.itemsCapacity);
        }

        public bool HaveNoItems(int id)
        {
           return walletEntity.items.Find(i => i.itemId == id).itemsCapacity == 0;
        }

        public bool HaveNoMoney()
        {
            return walletEntity.moneyBalance == 0;
        }

        public List<int> GetExistingItemsIds()
        {
            List<int> ids = new List<int>();
            walletEntity.items.ForEach(i => ids.Add(i.itemId));
            return ids;
        }

        public bool StackIsFull(int id)
        {
            return walletEntity.items.Find(i => i.itemId == id).itemsCapacity == walletEntity.maxItemCapacity;
        }
    }
}
