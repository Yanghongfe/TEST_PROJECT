using UnityEngine;

public interface ISlot
{
    RectTransform GetRectTransform();             // 用于子菜单定位
    ItemData GetCurrentItemData();                // 获取当前物品
    bool HasItem();                               // 是否有物品
    void RemoveItem();                            // 移除物品
}