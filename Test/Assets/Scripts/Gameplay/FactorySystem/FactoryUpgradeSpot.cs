using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FactoryUpgradeSpot : MonoBehaviour
{
    public int id;

    public event Action<int> OnUpgradeBuy;
    public event Action<int, bool> OnActive;


    private bool _isActive;

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUpgradeBuy?.Invoke(id);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = true;
            OnActive?.Invoke(id, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = false;
            OnActive?.Invoke(id, false);
        }
    }
}
