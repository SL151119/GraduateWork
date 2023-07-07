using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.FactorySystem.Configurations
{
    [Serializable, CreateAssetMenu(order = 0, fileName = "ItemsConfigsHolder")]
    public class ItemsConfigsHolder : ScriptableObject
    {
        public List<ItemConfigEntity> itemsConfigs;
    }
}
