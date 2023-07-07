using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.FactorySystem
{
    public class FactoryView : MonoBehaviour
    {
        [SerializeField] private Image consumableItemImage;
        [SerializeField] private TextMeshProUGUI label;

        private int _itemId;

        public int ItemId
        {
            get => _itemId;
            set => _itemId = value;
        }

        public void SetSprite(Sprite sprite)
        {
            consumableItemImage.sprite = sprite;
        }

        public void SetText(string text)
        {
            label.text = text;
        }
    }
}
