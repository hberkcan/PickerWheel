using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

[CreateAssetMenu(fileName = "New Harmful Item", menuName = "Item Data/Harmful Item")]
public class HarmfulObject : ItemDataObject
{
    public override void OnDrop(int amount)
    {
        MessagingSystem.Instance.Dispatch(new DestroyInventoryMessage());
    }
}
