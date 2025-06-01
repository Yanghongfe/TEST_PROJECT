using UnityEngine;
using UnityEngine.UI;

/*
 * This script only handles behavior of the inventory slots.
 * There are no logics, checks or validation for anything.
 */

public class InventorySlot : MonoBehaviour, ISlot
{
    public InventoryItem currentItem;

    public Image icon;
    public Text quantityText;
    public Text itemNameText;
    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public ItemData GetCurrentItemData()
    {
        return currentItem?.itemData;
    }

    public bool HasItem()
    {
        return currentItem != null;
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveSpecificItem(this, 1);
    }

    public void SetItem(InventoryItem item)
    {
        currentItem = item;

        icon.sprite = item.itemData.icon;
        quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        itemNameText.text = item.itemData.itemName;
    }

    public void ClearSlot()
    {
        currentItem = null;

        icon.sprite = null;
        quantityText.text = "";
        itemNameText.text = "";
    }

    public void ClickShowSubMenu()
    {
        SlotSubMenuManager.Instance.ShowSubMenu(this);
    }
}