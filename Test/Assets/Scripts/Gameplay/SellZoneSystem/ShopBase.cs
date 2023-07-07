using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBase : MonoBehaviour
{
    public ShopController shopController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopController.Activate(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopController.Activate(false);
        }
    }
}
