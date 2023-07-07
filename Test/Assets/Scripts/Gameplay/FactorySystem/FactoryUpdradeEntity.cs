using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FactoryUpdradeEntity
{
    public int factoryId;
    public int level;
    public List<int> upgradesCost;
    public List<int> capacityLevels;
    public List<float> productionSpeedLevels;
}
