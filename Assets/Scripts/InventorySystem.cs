using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System")]
public class InventorySystem : ScriptableObject
{
    private Dictionary<InventoryObject, InventorySlot> itemDictionary = new();
    [SerializeField] private List<InventorySlot> inventory = new();

    private Action onItemAdded;
    private Action onItemRemoved;

    public void Init(Action onItemAdded, Action onItemRemoved)
    {
        this.onItemAdded = onItemAdded;
        this.onItemRemoved = onItemRemoved;
    }

    public void Add(InventoryObject data, int amount)
    {
        if (itemDictionary.TryGetValue(data, out InventorySlot item))
        {
            onItemAdded?.Invoke();
            item.AddToStack(amount);
        }
        else
        {
            InventorySlot newItem = new InventorySlot(data, amount);
            onItemAdded?.Invoke();
            itemDictionary.Add(data, newItem);
            inventory.Add(newItem);
        }
    }

    public void Remove(InventoryObject data, int amount)
    {
        if (itemDictionary.TryGetValue(data, out InventorySlot item))
        {
            item.RemoveFromStack(amount);
            onItemRemoved?.Invoke();

            if (item.StackSize == 0)
            {
                itemDictionary.Remove(item.Data);
                inventory.Remove(item);
            }
        }
    }

    public void ClearInventory()
    {
        itemDictionary.Clear();
        inventory.Clear();
    }

    public void Reset()
    {
        ClearInventory();
    }
}
