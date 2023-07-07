using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.PlayerBehaviour
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private string itemCapacityTextTemplate;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemCapacityLabel;

        private int _id;

        public int Id
        {
            get => _id;
            set => _id = value;
        }
        
        public void SetIcon(Sprite sprite)
        {
            itemIcon.sprite = sprite;
        }

        public void SetText(int value)
        {
            itemCapacityLabel.text = String.Format(itemCapacityTextTemplate, value);
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}
