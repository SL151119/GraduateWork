using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem.Items;
using UnityEngine;

namespace Gameplay.PlayerBehaviour.InventorySystem
{
    public class BackPackSlot : MonoBehaviour
    {
        [SerializeField] private ItemView _itemInSlot;
        [SerializeField] private bool _slotIsBusy;

        public bool SlotIsBusy
        {
            get => _slotIsBusy;
            set => _slotIsBusy = value;
        }
        public ItemView ItemInSlot
        {
            get => _itemInSlot;
            set => _itemInSlot = value;

        }
    }
}
