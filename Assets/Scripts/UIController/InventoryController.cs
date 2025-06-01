using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryUI;                // 背包 UI 根对象

    [Header("Player Control")]
    public MonoBehaviour playerController;        // 玩家控制脚本（可禁用）

    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.Tab;       // 切换背包的按键
    public bool pauseGame = true;                 // 是否在背包打开时暂停游戏

    private bool isInventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        // 切换 UI 显示
        if (inventoryUI != null) inventoryUI.SetActive(isInventoryOpen);

        // 控制游戏暂停
        Time.timeScale = (pauseGame && isInventoryOpen) ? 0f : 1f;
    }
}
