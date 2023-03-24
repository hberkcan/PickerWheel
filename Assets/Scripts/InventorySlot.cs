using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public InventoryObject Data => data;
    [SerializeField]private InventoryObject data;
    public int StackSize => stackSize;
    [SerializeField] private int stackSize;

    public InventorySlot(InventoryObject data,int amount)
    {
        this.data = data;
        AddToStack(amount);
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;

        if (stackSize < 0)
            stackSize = 0;
    }
}
