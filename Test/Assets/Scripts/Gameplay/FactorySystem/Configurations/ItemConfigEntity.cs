using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.FactorySystem.Configurations
{
    [Serializable]
    public class ItemConfigEntity
    {
        public int id;
        public int price;
        public string name;
        public Sprite sprite;
    }

}
