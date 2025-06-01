using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public int id;          // Item ID
    public string itemName;   // Item name
    public Sprite icon;       // Item icon
    public ItemType itemType; // Item type
    public string description;// Item description
    public int maxStack; // Item max stack size
    public bool isEquippable; // Is the item equippable
}

public enum ItemType
{
    Consumable,
    Weapon,
    Armor,
    Helmet,
    Boots,
    Ring
}