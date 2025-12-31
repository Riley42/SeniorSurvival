using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingStationBehaviour : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] ItemSO craftingStationItem;        // Optional for player
    [SerializeField] List<RecipeSO> availableRecipes;   // Player OR station recipes
    [SerializeField] GameObject craftingWindow;

    [Header("Crafting Queue")]
    [SerializeField] List<CraftingQueueItem> currentCraftingQueue = new List<CraftingQueueItem>();

    bool isCraftingOpen = false;

    int amountToCraft = 1;
    RecipeSO currentRecipe;

    public RecipeSO CurrentRecipe => currentRecipe;
    public int AmountToCraft
    {
        get { return amountToCraft; }
        set { amountToCraft = value; }
    }

    void Start()
    {
        RecipeSlotData.OnRecipeSelected += HandleRecipeSelected;
        UIInputManager.Instance.OnCraftingToggled += TogglePlayerCrafting;
    }

    void OnDisable()
    {
        RecipeSlotData.OnRecipeSelected -= HandleRecipeSelected;
        UIInputManager.Instance.OnCraftingToggled -= TogglePlayerCrafting;
    }


    // ============================================================
    // PLAYER CRAFTING (pressing K)
    // ============================================================
    void TogglePlayerCrafting(bool isOpen)
    {
        if (craftingStationItem != null)
            return;

        if (isOpen)
            OpenCraftingWindow();
        else
            CloseCraftingWindow();
    }

    void OpenCraftingWindow()
    {
        isCraftingOpen = true;
        UIInputManager.Instance.HandleCursorLock(true);
        CraftingUI.Instance.Open(this, availableRecipes); //TODO: Make event
    }
    void CloseCraftingWindow()
    {
        isCraftingOpen = false;
        UIInputManager.Instance.HandleCursorLock(false);
        CraftingUI.Instance.Close(); //TODO: Make event
    }
    void HandleRecipeSelected(RecipeSO recipe)
    {
        if (!availableRecipes.Contains(recipe))
            return;

        currentRecipe = recipe;
        CraftingUI.Instance.ModifyCraftAmount(); //TODO: Make event
    }

    // ============================================================
    // Add to Queue
    // ============================================================
    public void AddToCraftingQueue()
    {
        if (CurrentRecipe == null) return;
        print($"Adding {CurrentRecipe.CreatedItem.Item.DisplayName} to queue");
    }

}

[System.Serializable]
public class CraftingQueueItem
{
    public RecipeSO Recipe;
    public int CraftingAmount;

    public CraftingQueueItem(RecipeSO recipe, int craftingAmount)
    {
        Recipe = recipe;
        CraftingAmount = craftingAmount;
    }
}