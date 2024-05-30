using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]

public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList _shopItemsHeld;
    [SerializeField] private ShopSystem _shopSystem;

    private ShopSaveData _shopSaveData;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;

    private string id;

    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopItemsHeld.Items.Count, _shopItemsHeld.MaxAllowedGold, _shopItemsHeld.BuyMarkUp, _shopItemsHeld.SellMarkUp);

        foreach (var item in _shopItemsHeld.Items)
        {
            Debug.Log($"{item.ItemData.DisplayName}: {item.Amount}");
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }

        id = GetComponent<UniqueID>().ID;
        _shopSaveData = new ShopSaveData(_shopSystem);
    }
    
    private void Start()
    {
        if (!SaveGameManager.data._shopKeeperDictionary.ContainsKey(id)) SaveGameManager.data._shopKeeperDictionary.Add(id, _shopSaveData);
    }

    private void OnEnable()
    {
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void OnDisable() 
    {
        SaveLoad.OnLoadGame -= LoadInventory;    
    }

    private void LoadInventory(SaveData data)
    {
        if (!data._shopKeeperDictionary.TryGetValue(id, out ShopSaveData shopSaveData)) return;

        _shopSaveData = shopSaveData;
        _shopSystem = _shopSaveData.ShopSystem;
    }

    public UnityAction<IInteractable> OnInteractionComplete { get; set;}

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
       var playerInv = interactor.GetComponent<PlayerInventoryHolder>();

       if (playerInv != null)
       {
            OnShopWindowRequested?.Invoke(_shopSystem, playerInv);
            interactSuccessful = true;
       }
       else
       {
        interactSuccessful = false;
        Debug.LogError("Player inventory not found");
       }
    }
    public void EndInteraction()
    {

    }
}


[System.Serializable]
public class ShopSaveData
{
    public ShopSystem ShopSystem;

    public ShopSaveData(ShopSystem shopSystem)
    {
        ShopSystem = shopSystem;
    }
}