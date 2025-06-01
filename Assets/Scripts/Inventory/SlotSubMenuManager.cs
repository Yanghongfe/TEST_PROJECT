using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * This script handles the sub-menu for inventory and equipment slots.
 * This also handles the logic for showing and hiding the sub-menu.
 * This also handles the logic for behavior of the sub-menu of the slots.
 */

public class SlotSubMenuManager : MonoBehaviour
{
    public static SlotSubMenuManager Instance; // Singleton instance

    public GameObject inventorySubMenu;
    public GameObject equipmentSubMenu;

    public ISlot currentSlot;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowSubMenu(ISlot slot)
    {
        currentSlot = slot;

        GameObject targetMenu = (currentSlot is InventorySlot) ? inventorySubMenu : equipmentSubMenu;
        SetupMenu(targetMenu, currentSlot);
    }

    public void SetupMenu(GameObject subMenu, ISlot slot)
    {
        RectTransform menuRect = subMenu.GetComponent<RectTransform>();
        RectTransform slotRect = slot.GetRectTransform();

        // Set the menu pivot to the bottom left corner
        menuRect.pivot = new Vector2(0f, 1f);

        // Set the menu size to match the slot size
        Vector2 realSize = new Vector2(
            slotRect.rect.width * slotRect.lossyScale.x,
            slotRect.rect.height * slotRect.lossyScale.y
        );
        menuRect.sizeDelta = realSize;

        // Set the menu position to match the slot position
        menuRect.position = slotRect.position;

        inventorySubMenu.SetActive(false);
        equipmentSubMenu.SetActive(false);
        subMenu.SetActive(true);
    }

    public void HideSubMenu()
    {
        inventorySubMenu.SetActive(false);
        equipmentSubMenu.SetActive(false);
        currentSlot = null;
    }

    public void RemoveItem()
    {
        if (currentSlot == null)
        {
            Debug.LogWarning("No slot selected.");
            HideSubMenu();
            return;
        }

        if (!currentSlot.HasItem())
        {
            Debug.LogWarning("Slot is empty.");
            HideSubMenu();
            return;
        }

        currentSlot.RemoveItem();
        InventoryManager.Instance.UpdateUI();
        HideSubMenu();
    }

    public void EquipItem()
    {
        if (currentSlot is not InventorySlot inventorySlot)
        {
            Debug.LogWarning("Equip only works on inventory slots.");
            HideSubMenu();
            return;
        }

        if (!inventorySlot.HasItem())
        {
            Debug.LogWarning("No item in this inventory slot.");
            HideSubMenu();
            return;
        }

        var itemData = inventorySlot.currentItem.itemData;

        if (itemData == null || !itemData.isEquippable)
        {
            Debug.LogWarning("Item is null or not equippable.");
            HideSubMenu();
            return;
        }

        var equipSlot = InventoryManager.Instance.FindEquippableSlot(inventorySlot.currentItem);
        if (equipSlot == null)
        {
            Debug.LogWarning("No suitable equipment slot found.");
            HideSubMenu();
            return;
        }

        equipSlot.EquipItem(itemData);
        inventorySlot.RemoveItem();
        InventoryManager.Instance.UpdateUI();
        HideSubMenu();
    }

    public void UnequipItem()
    {
        if (currentSlot is not EquipmentSlot equipmentSlot)
        {
            Debug.LogWarning("Unequip only works on equipment slots.");
            HideSubMenu();
            return;
        }

        var itemData = equipmentSlot.GetCurrentItemData();
        if (itemData == null)
        {
            Debug.LogWarning("No item equipped in this slot.");
            HideSubMenu();
            return;
        }

        equipmentSlot.UnequipItem();
        InventoryManager.Instance.UpdateUI();
        HideSubMenu();
    }
}
