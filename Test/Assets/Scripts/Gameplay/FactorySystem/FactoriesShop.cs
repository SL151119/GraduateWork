using Gameplay.FactorySystem;
using Gameplay.PlayerBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class FactoriesShop : MonoBehaviour
{
    public List<FactoryShopEntity> factoriesShopEntities;
    public List<FactoryGameObject> factories;
    public List<FactoryBuySpot> factoriesBuySpots;
    public List<FactoryUpgradeSpot> factoriesUpgradeSpot;
    public List<FactoryBase> factoriesEntities;
    public WalletController wallet;
    public PurchaseFactoryView view;
    [SerializeField] private FactoriesUpgradeConfig upgradeConfig;


    private void Awake()
    {
        factoriesBuySpots.ForEach(x =>
        {
            x.OnBuy += PurchaseFactory;
            x.OnActive += ActivateUI;
        });
        InitFactories();
    }

    private void InitFactories()
    {
        factories.ForEach(factory =>
        {
            factory.objectToDisable.SetActive(true);
            factory.objectToEnable.SetActive(false);
            if (factoriesShopEntities.Find(x => x.id == factory.id).unlocked)
            {
                factory.objectToDisable.SetActive(true);
                factory.objectToEnable.SetActive(true);
            }
        });
    }

    private void ActivateUpgradeUI(int id, bool state)
    {
        var factory = factoriesShopEntities.Find(x => x.id == id);
        var level = upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).level + 1;
        if (!factory.unlocked && level >= upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost.Count)
        {
            return;
        }
        if (state)
        {
            if(level < upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost.Count)
            {
                var upgrade = upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost[level];
                view.SetText("Do you really want to upgrade {0} for {1}?",factory.name, upgrade.ToString());
            }
        }

        view.Activate(state);
    }

    private void ActivateUI(int id, bool state)
    {
        var factoryEntity = factoriesShopEntities.Find(x => x.id == id);
        if (factoryEntity == null|| factoryEntity.unlocked)
        {
            return;
        }
        if (state)
        {
            view.SetText("Do you really want to buy {0} for {1}?" , factoryEntity.name, factoryEntity.price.ToString());
        }
        view.Activate(state);
    }

    private void PurchaseFactory(int id)
    {
        var factoryEntity = factoriesShopEntities.Find(x => x.id == id);
        if (factoryEntity.unlocked)
        {
            return;
        }
        if (wallet.Balance - factoryEntity.price >= 0)
        {
            factoriesUpgradeSpot.ForEach(x =>
            {
                if(x.id == id)
                {
                    x.OnActive += ActivateUpgradeUI;
                    x.OnUpgradeBuy += TryToUpgrade;
                }
            });
            factoriesShopEntities.Find(x => x.id == id).unlocked = true;
            InitFactories();
            wallet.DecreaseBalance(factoryEntity.price);
            ActivateUI(0, false);
            ActivateUpgradeUI(id, true);
        }
    }

    private void TryToUpgrade(int id)
    {
        var upgradeLevel = upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).level + 1;
        var factory = factoriesShopEntities.Find(x => x.id == id);
        if (!factory.unlocked && upgradeLevel >= upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost.Count)
        {
            return;
        }
        var upgradePrice = 0;
        if (upgradeLevel <= upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost.Count-1)
        {
            upgradePrice = upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost[upgradeLevel];
        }

        if (wallet.Balance - upgradePrice >= 0 && upgradeLevel < upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost.Count)
        {

            upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).level++;
            factoriesShopEntities.Find(x => x.id == id).unlocked = true;
            wallet.DecreaseBalance(upgradePrice);
            ActivateUpgradeUI(id, true);
            factoriesEntities.Find(x => x.Id == id).CheckUpgrades();
            if (upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).level >= upgradeConfig.upgradeEntities.Find(x => x.factoryId == id).upgradesCost.Count - 1)
            {
                ActivateUpgradeUI(id, false);
                factories.Find(x => x.id == id).objectToDisable.SetActive(false);
            }
        }
    }
}
