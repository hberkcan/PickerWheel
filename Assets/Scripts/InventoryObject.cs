using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Item Data/Inventory Item")]
public class InventoryObject : ItemDataObject
{
    public override void OnDrop(int amount)
    {
        MessagingSystem.Instance.Dispatch(new InventoryItemCollectMessage(this, amount));
    }
}
