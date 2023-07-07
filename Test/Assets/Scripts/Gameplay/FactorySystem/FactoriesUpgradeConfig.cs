using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable, CreateAssetMenu(order = 0, fileName = "FactoriesUpgradeSystem")]
public class FactoriesUpgradeConfig : ScriptableObject
{
    public List<FactoryUpdradeEntity> upgradeEntities;
}