using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class Player : MonoBehaviour, IMessagingSubscriber<InventoryItemCollectMessage>/*, IMessagingSubscriber<DestroyInventoryMessage>*/
{
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private CurrencyObject cash;
    [SerializeField] private CurrencyObject gold;

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<InventoryItemCollectMessage>(this);
        //MessagingSystem.Instance.Subscribe<DestroyInventoryMessage>(this);
        cash.OnValueChanged += Cash_OnValueChanged;
        gold.OnValueChanged += Gold_OnValueChanged;
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<InventoryItemCollectMessage>(this);
        //MessagingSystem.Instance.Unsubscribe<DestroyInventoryMessage>(this);
    }

    public void OnReceiveMessage(InventoryItemCollectMessage message)
    {
        inventory.Add((InventoryObject)message.Data, message.Amount);
    }

    //public void OnReceiveMessage(DestroyInventoryMessage message)
    //{
    //    inventory.ClearInventory();
    //}

    private void Cash_OnValueChanged()
    {
        Debug.Log($"Player Has {cash.Value} Cash");
    }

    private void Gold_OnValueChanged()
    {
        Debug.Log($"Player Has {gold.Value} Gold");
    }

    private void OnApplicationQuit()
    {
        inventory.Reset();
        cash.Reset();
        gold.Reset();
    }
}
