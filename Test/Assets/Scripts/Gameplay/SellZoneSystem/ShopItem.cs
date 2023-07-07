using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int id;
    public Action<int> OnSell;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Sell);
    }

    private void Sell()
    {
        OnSell?.Invoke(id);
    }
}