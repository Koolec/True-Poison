using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cauldron : MonoBehaviour, IPointerClickHandler
{
    public event Action<bool, bool> onClick;

    [SerializeField] private Image imageFirst;
    [SerializeField] private Image imageSecond;
    [SerializeField] private TextMeshProUGUI amountTextFirst;
    [SerializeField] private TextMeshProUGUI amountTextSecond;

    private void Awake()
    {
        Clear();
    }

    public void SetFirstIngredient(Sprite icon, int amount)
    {
        imageFirst.gameObject.SetActive(amount > 0);
        imageFirst.sprite = icon;
        amountTextFirst.text = amount > 0 ? amount.ToString() : "";
    }
    
    public void SetSecondIngredient(Sprite icon, int amount)
    {
        imageSecond.gameObject.SetActive(amount > 0);
        imageSecond.sprite = icon;
        amountTextSecond.text = amount > 0 ? amount.ToString() : "";
    }

    public void Clear()
    {
        imageFirst.gameObject.SetActive(false);
        imageFirst.sprite = null;
        
        imageSecond.gameObject.SetActive(false);
        imageSecond.sprite = null;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var isRightButton = eventData.button == PointerEventData.InputButton.Right;
        var isRightSide = false;
        Vector2 mousePosition = eventData.pointerCurrentRaycast.screenPosition;
        
        if (mousePosition.x > Screen.width / 2f)
        {
            isRightSide = true;
        }

        onClick?.Invoke(isRightButton, isRightSide);
    }
}
