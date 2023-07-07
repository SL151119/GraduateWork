using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem.Configurations;
using Gameplay.FactorySystem.Items;
using Gameplay.FactorySystem.Storage;
using UnityEngine;

namespace Gameplay.FactorySystem
{
    public class FactoryBase : MonoBehaviour
    {
        [SerializeField] private ItemsConfigsHolder itemsConfigsHolder;
        [SerializeField] private FactoryEntity factoryEntity;
        [SerializeField] private FactoryView factoryView;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private Transform instantiateItemsPoint;

        public int Id => factoryEntity.Id;

        public void CheckUpgrades()
        {
            factoryEntity.UpdateUpgrades();
        }

        private void Start()
        {
            factoryEntity.OnGrabItem += AnimateItemIn;
            factoryEntity.OnPutItem += AnimateItemOut;
            InitializeFactory();
        }

        private void OnDestroy()
        {
            factoryEntity.OnGrabItem -= AnimateItemIn;
            factoryEntity.OnPutItem -= AnimateItemOut;
        }

        private void InitializeFactory()
        {
            factoryEntity.InitializeStorages();
            factoryEntity.InitializeFactory();
            InitializeView();

            StartCoroutine(ManufactureCoroutine());
        }

        private void InitializeView()
        {
            factoryView.SetSprite(itemsConfigsHolder.itemsConfigs.Find(c => c.id == factoryEntity.manufacturedItems[0].id).sprite);
        }

        private IEnumerator ManufactureCoroutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => !factoryEntity.StorageToFillIsFull() && factoryEntity.StorageToGrabHasItems());
                factoryEntity.GrabItems();
                yield return new WaitForSeconds(factoryEntity.ManufactureDuration);
                factoryEntity.PutItems();
            }
        }


        private void AnimateItemIn(int id, Transform startTransform)
        {
            //var currentItem = itemsPool.GetFreeItemById(id);
            //itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(currentItem, startTransform, transform, true);
        } 
        
        private void AnimateItemOut(int id, Transform endTransform)
        {
            var currentItem = itemsPool.GetFreeItemById(id);
            itemsPool.ItemsVisualAnimation.InstantiateItemOnPoint(currentItem, instantiateItemsPoint);
        }
        
        
    }
}
