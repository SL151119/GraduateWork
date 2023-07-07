using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.FactorySystem.Items;
using Gameplay.FactorySystem.Storage;
using UnityEngine;

namespace Gameplay.FactorySystem
{
    [Serializable]
    public class FactoryEntity
    {
        [SerializeField] private int id;
        [SerializeField] private FactoriesUpgradeConfig factoriesUpgradeConfig;
        [Header("Produce settings")]
        [SerializeField] private float manufactureDuration;
        [SerializeField] private ConveyorBelt conveyor;
        [Space]
        [Header("Items settings")]
        public List<Item> consumableItems;
        public List<Item> manufacturedItems;
        
        [Space] 
        [Header("Storages settings")] 
        public StorageBase storageToFill;
        public StorageBase storageToGrab;

        public event Action<int, Transform> OnGrabItem;
        public event Action<int, Transform> OnPutItem;

        public float ManufactureDuration => manufactureDuration;

        public int Id => id;
        

        public void InitializeStorages()
        {
            if (HasConsumableItems())
            {
                storageToGrab.InitializeStorage(consumableItems);
            }
            
            storageToFill.InitializeStorage(manufacturedItems);
        }

        public bool HasConsumableItems()
        {
            return consumableItems.Any();
        }

        public bool StorageToGrabHasItems()
        {
            foreach (var item in consumableItems)
            {
                if (!storageToGrab.HasItems(item.id))
                {
                    conveyor.SetActive(false);
                    return false;
                }
            }

            conveyor.SetActive(true);
            return true;
        }

        public bool StorageToFillIsFull()
        {
            conveyor.SetActive(!storageToFill.StorageIsFull());
            return  storageToFill.StorageIsFull();
        }

        public void GrabItems()
        {
            consumableItems.ForEach(i =>
            {
                storageToGrab.GrabFromStorage(i.id, 1);
                OnGrabItem?.Invoke(i.id, storageToGrab.transform);
            });
        }

        public void PutItems()
        {
            manufacturedItems.ForEach(i =>
            {
                storageToFill.FillStorage(i.id, 1);
                OnPutItem?.Invoke(i.id, storageToFill.transform);
            });
        }

        public void InitializeFactory()
        {
            conveyor.SetActive(false);
            var factoryUpgradeConfig = factoriesUpgradeConfig.upgradeEntities.Find(upgrade => upgrade.factoryId == id);
            var manufactureSpeed = factoryUpgradeConfig.productionSpeedLevels[factoryUpgradeConfig.level];
            var capacity = factoryUpgradeConfig.capacityLevels[factoryUpgradeConfig.level];
            manufactureDuration = manufactureSpeed;
            storageToFill.InitializeCapacity(capacity);
            storageToGrab.InitializeCapacity(capacity);
        }

        public void UpdateUpgrades()
        {
            var factoryUpgradeConfig = factoriesUpgradeConfig.upgradeEntities.Find(upgrade => upgrade.factoryId == id);
            var manufactureSpeed = factoryUpgradeConfig.productionSpeedLevels[factoryUpgradeConfig.level];
            var capacity = factoryUpgradeConfig.capacityLevels[factoryUpgradeConfig.level];
            manufactureDuration = manufactureSpeed;
            storageToFill.InitializeCapacity(capacity);
            storageToGrab.InitializeCapacity(capacity);
        }
    }
}
