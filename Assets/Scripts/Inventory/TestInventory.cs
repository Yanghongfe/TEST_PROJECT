using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public ItemData testItem; // Assign this in the inspector

    public void AddTestItem()
    {
        Debug.Log("Adding test item to inventory: " + testItem.itemName);
        InventoryManager.Instance.AddItem(testItem, 1);
        InventoryManager.Instance.UpdateUI();
    }
}