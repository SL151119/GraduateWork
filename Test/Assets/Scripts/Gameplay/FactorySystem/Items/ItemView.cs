using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.FactorySystem.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private GameObject itemVariant;
        
        public int Id => id;
    }   
}
