namespace MyMessagingSystem
{
    public struct InventoryItemCollectMessage
    {
        public ItemDataObject Data;
        public int Amount;

        public InventoryItemCollectMessage(ItemDataObject data, int amount)
        {
            Data = data;
            Amount = amount;
        }
    }
}