using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton instance
    public int maxSlots; // Inventory max slots
    public List<InventoryItem> inventoryItems = new List<InventoryItem>(); // Inventory items

    public Transform inventorySlotParent;
    public InventorySlot inventorySlotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>(); // Set in Inspector. Equipment item references saved in this list.

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < Instance.maxSlots; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, inventorySlotParent);
            slot.ClearSlot();
            inventorySlots.Add(slot);
        }
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            InventoryManager.Instance.PrintEquipmentSlots();
            // InventoryManager.Instance.PrintItems();
            // InventoryManager.Instance.PrintSlots();
        }
    }

    public void PrintItems()
    {
        Debug.Log("当前背包中的物品列表：");

        if (inventoryItems.Count == 0)
        {
            Debug.Log("背包是空的！");
            return;
        }

        foreach (InventoryItem item in inventoryItems)
        {
            Debug.Log($"[{Time.time}] {item.itemData.itemName} - 数量: {item.quantity}");
        }
    }

    public void PrintSlots()
    {
        Debug.Log("当前 UI Slots 状态：");

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            string slotStatus = (inventorySlots[i].currentItem != null) ? 
                $"{inventorySlots[i].currentItem.itemData.itemName}" : "空";

            Debug.Log($"[{Time.time}] Slot {i}: {slotStatus}");
        }
    }

    public void PrintEquipmentSlots()
    {
        Debug.Log("当前 Equipment Slots 状态：");

        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            string slotStatus = (equipmentSlots[i].equippedItemData != null) ?
                $"{equipmentSlots[i].equippedItemData.itemName}" : "空";

            Debug.Log($"[{Time.time}] Slot {i}: {slotStatus}\n");
            Debug.Log($"Slot {i}: {equipmentSlots[i].allowedType}");
        }
    }


    public void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i < Instance.inventorySlots.Count)
                if (inventoryItems.Count > i)
                {
                    inventorySlots[i].SetItem(Instance.inventoryItems[i]);
                }
            else
                inventorySlots[i].ClearSlot();
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        // Check if the item already exists in the inventory
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.itemData == item && inventoryItem.quantity < item.maxStack)
            {
                int spaceLeft = item.maxStack - inventoryItem.quantity;
                int addAmount = Mathf.Min(spaceLeft, amount);
                inventoryItem.quantity += addAmount;
                amount -= addAmount;

                if (amount <= 0) return true;
            }
        }

        // Add new item if it doesn't exist or if there's no space left in the existing stack
        if (inventoryItems.Count < maxSlots)
        {
            inventoryItems.Add(new InventoryItem(item, amount));
            return true;
        }

        return false; // Inventory is full
    }

    public void RemoveSpecificItem(InventorySlot slot, int amount)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] == slot)
            {
                if (inventoryItems[i].quantity > amount)
                {
                    inventoryItems[i].quantity -= amount;
                    return;
                }
                else
                {
                    inventoryItems.RemoveAt(i);
                    return;
                }
            }
        }
    }

    public EquipmentSlot FindEquippableSlot(InventoryItem inventoryItem)
    {   
        if (inventoryItem.itemData.isEquippable) 
        {
            foreach (var equipmentSlot in equipmentSlots)
            {
                Debug.Log($"装备槽: {equipmentSlot} \n" +
                            $"允许的物品类型: {equipmentSlot.allowedType} \n" +
                            $"物品类型: {inventoryItem.itemData.itemType}");
                if (equipmentSlot.allowedType == inventoryItem.itemData.itemType)
                {
                    return equipmentSlot;
                }
            }
        }
        Debug.LogWarning($"没有找到合适的装备槽来装备 {inventoryItem.itemData.itemName}.");
        return null;
    }

    public bool HasItem(ItemData item, int amount)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.itemData == item && inventoryItem.quantity >= amount)
                return true;
        }
        return false;
    }
}