using TMPro;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeSlotData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RecipeSO content;
    [SerializeField] TextMeshProUGUI displayName;

    public static event Action<RecipeSO> OnRecipeSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRecipeSelected?.Invoke(content);
    }

    public void SetContentData(RecipeSO recipe)
    {
        content = recipe;
        displayName.text = content.CreatedItem.Item.DisplayName;
    }
}
