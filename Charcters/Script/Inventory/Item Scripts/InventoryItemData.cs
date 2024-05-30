using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]

public class InventoryItemData : ScriptableObject
{
   public int ID = -1;
   public string DisplayName;
   [TextArea(4, 4)]
   public string Description; 
   public Sprite Icon;
   public int MaxStackSize;
   public int GoldValue;
   public GameObject ItemPrefab;
}
