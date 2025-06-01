using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

/*
 * This script only handles behavior of the equipment slots.
 * There are no logics, checks or validation for anything.
 */


public class EquipmentSlot : MonoBehaviour, ISlot
{
    public ItemType allowedType; // Allowed item type for this slot
    public ItemData equippedItemData;

    public Image icon;
    public Text itemNameText;
    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public ItemData GetCurrentItemData()
    {
        return equippedItemData;
    }
    public bool HasItem()
    {
        return equippedItemData != null;
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.AddItem(equippedItemData, 1);
        equippedItemData = null;
    }

    public void SetItem(ItemData itemData)
    {
        equippedItemData = itemData;

        icon.sprite = itemData.icon;
        itemNameText.text = itemData.itemName;
    }

    public void ClearSlot()
    {
        equippedItemData = null;

        icon.sprite = null;
        itemNameText.text = "No item";
    }

    public void EquipItem(ItemData newItem)
    {
        SetItem(newItem);
    }

    public void UnequipItem()
    {
        RemoveItem();
        ClearSlot();
    }
    public void ClickShowSubMenu()
    {
        SlotSubMenuManager.Instance.ShowSubMenu(this);
    }
}
