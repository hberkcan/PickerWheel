using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;
using System;

[CreateAssetMenu(fileName = "New Currency Item", menuName = "Item Data/Currency Item")]
public class CurrencyObject : ItemDataObject
{
    public int Value
    {
        get { return value; }
        private set 
        {
            if (this.value == value)
                return;

            this.value = value;

            if (this.value < 0)
                this.value = 0;

            OnValueChanged.Invoke();
        }
    }

    [SerializeField] private int value;
    public event Action OnValueChanged;

    public void Spend(int amount)
    {
        Value -= amount;
    }

    public void Earn(int amount)
    {
        Value += amount;
    }

    public bool IsAffordable(int price)
    {
        return Value >= price;
    }

    public override void OnDrop(int amount)
    {
        Value += amount;
    }

    public void Reset()
    {
        Value = 0;
    }
}
