using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
   
    public static UnityAction OnPlayerInventoryChanged;

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.invSystem != null)
        { 
            this.primaryInventorySystem = data.playerInventory.invSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
    }
    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

        return false;
    }
}
