using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.FactorySystem.Storage
{
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private string capacityLabelTemplate;
        [SerializeField] private Image image;
        [SerializeField] private Image warningMarkImage;
        [SerializeField] private TextMeshProUGUI capacityLabel;

        private int _itemId;

        public int ItemId
        {
            get => _itemId;
            set => _itemId = value;
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }
        
        public void UpdateView(int value)
        {
            capacityLabel.text = string.Format(capacityLabelTemplate, value);
        }

        public void SetFullMarkActive(bool state)
        {
            warningMarkImage.gameObject.SetActive(state);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }
    }
}
