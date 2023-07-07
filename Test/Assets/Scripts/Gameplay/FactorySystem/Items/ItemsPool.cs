using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Gameplay.FactorySystem.Items
{
    public class ItemsPool : MonoBehaviour
    {
        [SerializeField] private List<ItemView> itemsViews;
        [SerializeField] private ItemsVisualAnimation itemsAnimation;
        [SerializeField] private List<ItemView> itemsToInstantiate;

        private void Awake()
        {
            itemsToInstantiate.ForEach(x =>
            {
                for(int i = 0; i < 200; i++)
                {
                    var item = Instantiate(x, transform);
                    item.gameObject.SetActive(false);
                    itemsViews.Add(item);
                }
            });
        }

        public ItemsVisualAnimation ItemsVisualAnimation => itemsAnimation;

        public ItemView GetFreeItemById(int id)
        {
            var currentItem = itemsViews.FindAll(i => !i.gameObject.activeSelf).Find(i => i.Id == id);
            currentItem.transform.parent = null;
            
            return currentItem;
        }
    }
}
