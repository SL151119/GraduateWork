using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchaseFactoryView : MonoBehaviour
{
    [SerializeField] private string textTemplate;
    [SerializeField] private TextMeshProUGUI label;

    public void SetText(string template, string name, string price)
    {
        label.text = string.Format(template, name, price);
    }

    public void Activate(bool state)
    {
        gameObject.SetActive(state);
    } 
}
