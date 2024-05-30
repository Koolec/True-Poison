using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftPanel : MonoBehaviour
{
    [SerializeField] private List<Sprite> ingredientIcons;
    [SerializeField] private Cauldron cauldron;
    [SerializeField] private MouseItemData mouseInventoryItem;
    [SerializeField] private List<InventoryItemData> craftedItems;
    [SerializeField] private List<InventoryItemData> ingredientItems;

    private KeyValuePair<int, int> itemIdFirst = new (-1, 0);
    private KeyValuePair<int, int> itemIdSecond = new (-1, 0);

    private void Awake()
    {
        cauldron.onClick += OnCauldronClick;
    }

    private void OnDestroy()
    {
        cauldron.onClick -= OnCauldronClick;
    }
    
    private void Update()
    {
        if (gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame) 
            gameObject.SetActive(false);
    }

    private void OnCauldronClick(bool isRightButton, bool isRightSide)
    {
        if (isRightButton)
        {
            ProcessRemoveItem(isRightSide);
            return;
        }
        
        if (mouseInventoryItem.AssignedInventorySlot == null 
            || mouseInventoryItem.AssignedInventorySlot.ItemData == null 
            || mouseInventoryItem.AssignedInventorySlot.ItemData.ID > 2)
            return;

        var isAdded = false;
        if (isRightSide)
        {
            if ((itemIdFirst.Key == -1 && mouseInventoryItem.AssignedInventorySlot.ItemData.ID != itemIdSecond.Key)
                || itemIdFirst.Key == mouseInventoryItem.AssignedInventorySlot.ItemData.ID)
            {
                itemIdFirst = new KeyValuePair<int, int>(mouseInventoryItem.AssignedInventorySlot.ItemData.ID, itemIdFirst.Value + 1);
                cauldron.SetFirstIngredient(ingredientIcons[itemIdFirst.Key], itemIdFirst.Value);
                isAdded = true;
            }
            else if ((itemIdSecond.Key == -1  && mouseInventoryItem.AssignedInventorySlot.ItemData.ID != itemIdFirst.Key)
                     || itemIdSecond.Key == mouseInventoryItem.AssignedInventorySlot.ItemData.ID)
            {
                itemIdSecond = new KeyValuePair<int, int>(mouseInventoryItem.AssignedInventorySlot.ItemData.ID, itemIdSecond.Value + 1);
                cauldron.SetSecondIngredient(ingredientIcons[itemIdSecond.Key], itemIdSecond.Value);
                isAdded = true;
            }
        }
        else
        {
            if ((itemIdSecond.Key == -1  && mouseInventoryItem.AssignedInventorySlot.ItemData.ID != itemIdFirst.Key)
                || itemIdSecond.Key == mouseInventoryItem.AssignedInventorySlot.ItemData.ID)
            {
                itemIdSecond = new KeyValuePair<int, int>(mouseInventoryItem.AssignedInventorySlot.ItemData.ID, itemIdSecond.Value + 1);
                cauldron.SetSecondIngredient(ingredientIcons[itemIdSecond.Key], itemIdSecond.Value);
                isAdded = true;
            }
            else if ((itemIdFirst.Key == -1 && mouseInventoryItem.AssignedInventorySlot.ItemData.ID != itemIdSecond.Key)
                     || itemIdFirst.Key == mouseInventoryItem.AssignedInventorySlot.ItemData.ID)
            {
                itemIdFirst = new KeyValuePair<int, int>(mouseInventoryItem.AssignedInventorySlot.ItemData.ID, itemIdFirst.Value + 1);
                cauldron.SetFirstIngredient(ingredientIcons[itemIdFirst.Key], itemIdFirst.Value);
                isAdded = true;
            }
        }

        if (isAdded)
        {
            if (mouseInventoryItem.AssignedInventorySlot.StackSize > 1)
            {
                mouseInventoryItem.AssignedInventorySlot.AddToStack(-1);
                mouseInventoryItem.UpdateMouseSlot();
            }
            else
            {
                mouseInventoryItem.ClearSlot();
            }
        }
    }

    private void ProcessRemoveItem(bool isRightSide)
    {
        if (isRightSide)
        {
            if (itemIdFirst.Key >= 0)
            {
                itemIdFirst = new KeyValuePair<int, int>(itemIdFirst.Key, itemIdFirst.Value - 1);
                SaveGameManager.data.playerInventory.invSystem.AddToInventory(ingredientItems[itemIdFirst.Key], 1);
                if (itemIdFirst.Value <= 0)
                    itemIdFirst = new KeyValuePair<int, int>(-1, 0);
                
                cauldron.SetFirstIngredient(itemIdFirst.Key >= 0 ? ingredientIcons[itemIdFirst.Key] : null, itemIdFirst.Value);
            }
            else if (itemIdSecond.Key >= 0)
            {
                itemIdSecond = new KeyValuePair<int, int>(itemIdSecond.Key, itemIdSecond.Value - 1);
                SaveGameManager.data.playerInventory.invSystem.AddToInventory(ingredientItems[itemIdSecond.Key], 1);
                if (itemIdSecond.Value <= 0)
                    itemIdSecond = new KeyValuePair<int, int>(-1, 0);
                
                cauldron.SetSecondIngredient(itemIdSecond.Key >= 0 ? ingredientIcons[itemIdSecond.Key] : null, itemIdSecond.Value);
            }
        }
        else
        {
            if (itemIdSecond.Key >= 0)
            {
                itemIdSecond = new KeyValuePair<int, int>(itemIdSecond.Key, itemIdSecond.Value - 1);
                SaveGameManager.data.playerInventory.invSystem.AddToInventory(ingredientItems[itemIdSecond.Key], 1);
                if (itemIdSecond.Value <= 0)
                    itemIdSecond = new KeyValuePair<int, int>(-1, 0);
                
                cauldron.SetSecondIngredient(itemIdSecond.Key >= 0 ? ingredientIcons[itemIdSecond.Key] : null, itemIdSecond.Value);
            }
            else if (itemIdFirst.Key >= 0)
            {
                itemIdFirst = new KeyValuePair<int, int>(itemIdFirst.Key, itemIdFirst.Value - 1);
                SaveGameManager.data.playerInventory.invSystem.AddToInventory(ingredientItems[itemIdFirst.Key], 1);
                if (itemIdFirst.Value <= 0)
                    itemIdFirst = new KeyValuePair<int, int>(-1, 0);
                
                cauldron.SetFirstIngredient(itemIdFirst.Key >= 0 ? ingredientIcons[itemIdFirst.Key] : null, itemIdFirst.Value);
            }
        }
    }

    public void OnCraft()
    {
        if (itemIdFirst.Key < 0 || itemIdSecond.Key < 0)
            return;
        
        int finalItemId = -1;
        int remainAmountId = -1;
        int remainAmount = 0;
        if (itemIdFirst.Value > itemIdSecond.Value)
        {
            remainAmount = itemIdFirst.Value - itemIdSecond.Value;
            remainAmountId = itemIdFirst.Key;
        }
        else if (itemIdFirst.Value < itemIdSecond.Value)
        {
            remainAmount = itemIdSecond.Value - itemIdFirst.Value;
            remainAmountId = itemIdSecond.Key;
        }
        
        int finalAmount = Mathf.Min(itemIdFirst.Value, itemIdSecond.Value);
        
        if (finalAmount <= 0)
            return;

        if ((itemIdFirst.Key == 0 && itemIdSecond.Key == 1) || (itemIdFirst.Key == 1 && itemIdSecond.Key == 0))
        {
            finalItemId = 0;
        }
        else if ((itemIdFirst.Key == 0 && itemIdSecond.Key == 2) || (itemIdFirst.Key == 2 && itemIdSecond.Key == 0))
        {
            finalItemId = 1;
        }
        else
        {
            finalItemId = 2;
        }
        
        cauldron.Clear();
        itemIdFirst = new KeyValuePair<int, int>(-1, 0);;
        itemIdSecond = new KeyValuePair<int, int>(-1, 0);;

        while (finalAmount > 0)
        {
            SaveGameManager.data.playerInventory.invSystem.AddToInventory(craftedItems[finalItemId], 1);
            finalAmount--;
        }

        if (remainAmountId >= 0)
        {
            while (remainAmount > 0)
            {
                SaveGameManager.data.playerInventory.invSystem.AddToInventory(ingredientItems[remainAmountId], 1);
                remainAmount--;
            }
        }
    }
}
