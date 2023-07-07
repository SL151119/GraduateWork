using Gameplay.FactorySystem.Configurations;
using Gameplay.PlayerBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private WalletController walletController;
    [SerializeField] private ItemsConfigsHolder itemsConfigsHolder;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button sellAllButton;

    private void Start()
    {
        InitializeShop();
        exitButton.onClick.AddListener(() => Activate(false));
        sellAllButton.onClick.AddListener(SellAll);
    }

    public void Activate(bool state)
    {
        gameObject.SetActive(state);
    }

    private void InitializeShop()
    {
        shopItems.ForEach(x =>
        {
            x.OnSell += TryToSell;
        });
    }

    private void TryToSell(int id)
    {
        if (walletController.HaveNoItems(id))
        {
           return;
        }
        walletController.IncreaseBalance(itemsConfigsHolder.itemsConfigs.Find(i => i.id == id).price);
        walletController.DecreaseWallet(id, 1);
    }

    private void SellAll()
    {
        itemsConfigsHolder.itemsConfigs.ForEach(x =>
        {
            if (!walletController.HaveNoItems(x.id))
            {
                walletController.IncreaseBalance(x.price * walletController.GetTotalCapacity(x.id));
                walletController.DecreaseWallet(x.id, walletController.GetTotalCapacity(x.id));
            }
        });
    }
}
