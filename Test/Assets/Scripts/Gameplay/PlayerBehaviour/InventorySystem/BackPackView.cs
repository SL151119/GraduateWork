using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.FactorySystem.Items;
using UnityEngine;

namespace Gameplay.PlayerBehaviour.InventorySystem
{
    public class BackPackView : MonoBehaviour
    {
        [SerializeField] private List<BackPackSlot> backPackSlots;

        public Transform AddItem(ItemView item)
        {
            var currentSlot = backPackSlots.Find(x => !x.SlotIsBusy);
            currentSlot.ItemInSlot = item;
            currentSlot.SlotIsBusy = true;
            item.transform.parent = currentSlot.transform;
            return currentSlot.transform;
        }

        public bool AllSlotsAreBusy()
        {
            return backPackSlots.FindAll(x => x.SlotIsBusy).Count == backPackSlots.Count;
        }

        public Transform RemoveItem(int id)
        {
            var currentSlot = backPackSlots.Find(x => x.SlotIsBusy && x.ItemInSlot.Id == id);
            currentSlot.ItemInSlot.transform.parent = null;
            currentSlot.ItemInSlot = null;
            currentSlot.SlotIsBusy = false;

            return currentSlot.transform;
        }

        public ItemView GetItemById(int id)
        {
            var backPackSlot = backPackSlots.Find(x => x.SlotIsBusy && x.ItemInSlot.Id == id);
            return backPackSlot.ItemInSlot;
        }

        public bool ItemByIdIsExist(int id)
        {
            return backPackSlots.Find(x => x.SlotIsBusy && x.ItemInSlot.Id == id) != null;
        }
        
        
    }
}
