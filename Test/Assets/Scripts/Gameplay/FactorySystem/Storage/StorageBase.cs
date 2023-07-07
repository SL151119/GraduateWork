using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem.Configurations;
using Gameplay.FactorySystem.Items;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.FactorySystem.Storage
{
    public class StorageBase : MonoBehaviour
    {
        [SerializeField] private StorageTypes type;
        [SerializeField] private ItemsConfigsHolder itemsConfigHolder;
        [SerializeField] private StorageEntity storageEntity;
        [SerializeField] private List<StorageView> storageView;

        private List<StorageView> _activeStorageViews;

        public event Action<StorageBase, List<int>, int> OnPlayerEnter;
        public event Action OnPlayerExit;

        public StorageTypes StorageTypes => type;

        public void InitializeStorage(List<Item> items)
        {
            storageEntity.SetItemsToStorage(items);
            
            InitializeViews();
        }

        public void FillStorage(int id, int value)
        {
            var currentStorage = storageEntity.ItemsHolder.Find(i => i.itemId == id);
            currentStorage.itemsCapacity += value;
            var currentView = storageView.Find(v => v.ItemId == id);
            currentView.UpdateView(currentStorage.itemsCapacity);
            //currentView.SetFullMarkActive(StorageIsFull());
        }
        
        public void GrabFromStorage(int id, int value)
        {
            var currentStorage = storageEntity.ItemsHolder.Find(i => i.itemId == id);
            currentStorage.itemsCapacity -= value;
            var currentView = storageView.Find(v => v.ItemId == id);
            currentView.UpdateView(currentStorage.itemsCapacity);
            //currentView.SetFullMarkActive(StorageIsFull());
        }
        
        public bool StorageIsFull()
        {
            return GetTotalCapacity() == storageEntity.MaxCapacity;
        }

        public bool StorageIsEmpty()
        {
            return GetTotalCapacity() == 0;
        }

        public bool HasItems(int id)
        {
            return GetCapacityByItemId(id) > 0;
        }

        public bool FullOfItemsById(int id)
        {
            var currentItem = storageEntity.ItemsHolder.Find(i => i.itemId == id);
            return currentItem.itemsCapacity == currentItem.maxCapacity;
        }

        public void InitializeCapacity(int currentValue)
        {
            storageEntity.InitializeMaxCapacity(currentValue);
        }
        
        private int GetTotalCapacity()
        {
            var currentCapacity = 0;
            storageEntity.ItemsHolder.ForEach(i => currentCapacity += i.itemsCapacity);
            return currentCapacity;
        }

        private int GetCapacityByItemId(int id)
        {
            return storageEntity.ItemsHolder.Find(i => i.itemId == id).itemsCapacity;
        }

        private void InitializeViews()
        {
            _activeStorageViews = new List<StorageView>();
            storageEntity.ItemsHolder.ForEach(x =>
            {
                var currentView = storageView.Find(v => !v.IsActive());
                currentView.Activate();
                currentView.ItemId = x.itemId;
                currentView.SetSprite(itemsConfigHolder.itemsConfigs.Find(c => c.id == currentView.ItemId).sprite);
                currentView.UpdateView(x.itemsCapacity);
                _activeStorageViews.Add(currentView);
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnter?.Invoke(this, GetListOfIds(), 1);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerExit?.Invoke();
            }
        }

        private List<int> GetListOfIds()
        {
            var ids = new List<int>();
            storageEntity.ItemsHolder.ForEach(i =>
            {
                ids.Add(i.itemId);
            });
            return ids;
        }
    }
}
