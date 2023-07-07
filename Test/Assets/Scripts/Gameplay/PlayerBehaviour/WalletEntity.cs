using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem.Items;
using UnityEngine;

namespace Gameplay.PlayerBehaviour
{
    [Serializable]
    public class WalletEntity
    {
        public int moneyBalance;
        public int maxItemCapacity;
        public List<ItemsHolder> items;
    }
}
