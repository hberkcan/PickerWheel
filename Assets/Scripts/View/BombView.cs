using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombView : View
{
    [SerializeField] private Button giveUpButton;

    [SerializeField] private Button reviveButton;
    [SerializeField] private int revivePrice;
    [SerializeField] private TextMeshProUGUI revivePriceText;

    [SerializeField] private Button reviveAdButton;

    [SerializeField] private InventorySystem inventory;
    [SerializeField] private CurrencyObject gold;
    [SerializeField] private IntVariable zoneCount;

    public override void Init()
    {
        giveUpButton.onClick.AddListener(() => {
            inventory.ClearInventory();
            zoneCount.Value = 1;
            ViewManager.Instance.Show<PickerWheelView>();
        });

        revivePriceText.text = revivePrice.ToString();
        reviveButton.onClick.AddListener(() =>
        {
            if (gold.IsAffordable(revivePrice))
            {
                gold.Spend(revivePrice);
                ViewManager.Instance.Show<PickerWheelView>();
            }
        });
    }
}
