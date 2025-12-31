using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CraftingUI : Singleton<CraftingUI>
{
    [Header("Recipe Window")]
    [SerializeField] Transform recipeContainer;
    [SerializeField] GameObject recipePrefab;
    [SerializeField] List<GameObject> recipeSlotList = new List<GameObject>();

    [Header("Description Window")]
    [SerializeField] Image createdItemIcon;
    [SerializeField] TextMeshProUGUI createdItemName;
    [SerializeField] TextMeshProUGUI createdItemDesc;
    [SerializeField] TMP_InputField craftAmountText;
    [SerializeField] TextMeshProUGUI craftTimeText;
    [SerializeField] List<GameObject> materialSlotList = new List<GameObject>();

    CraftingStationBehaviour currentStation;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Open(CraftingStationBehaviour station, List<RecipeSO> recipes)
    {
        currentStation = station;
        gameObject.SetActive(true);
        RecreateCraftingUI(recipes);
        //currentStation.OnDisplayRecipeData += UpdateDescriptionUI;
    }

    public void Close()
    {
        if (currentStation != null)
        {
            //currentStation.OnDisplayRecipeData -= UpdateDescriptionUI;
            currentStation = null;
        }

        gameObject.SetActive(false);
    }

    public void RecreateCraftingUI(List<RecipeSO> recipes = null)
    {
        foreach(var slot in recipeSlotList)
        {
            Destroy(slot);
        }
        recipeSlotList.Clear();
        FillCraftingMenu(recipes);
    }
    private void FillCraftingMenu(List<RecipeSO> recipes = null)
    {
        foreach (RecipeSO recipe in recipes)
        {
            GameObject slot = Instantiate(recipePrefab, recipeContainer);
            recipeSlotList.Add(slot);
            slot.GetComponent<RecipeSlotData>().SetContentData(recipe);
        }
    }

    public void ModifyCraftAmount(int modifier = 0)
    {
        var amountToCraft = 0;
        if (modifier == 1 && amountToCraft < 99)
        {
            amountToCraft++;
        }
        else if (modifier == -1 && amountToCraft > 1)
        {
            amountToCraft--;
        }
        else
        {
            amountToCraft = 1;
        }

        currentStation.AmountToCraft = amountToCraft;
        UpdateDescriptionUI(currentStation.CurrentRecipe, amountToCraft);
    }

    private void FillDescriptionUI(RecipeSO recipe, int multiplier = 1)
    {
        createdItemIcon.sprite = recipe.CreatedItem.Item.Icon;
        createdItemIcon.color = new Color(1, 1, 1, 1);
        createdItemName.text = recipe.CreatedItem.Item.DisplayName;
        //createdItemDesc.text = recipe.CreatedItem.Item.ItemDescription;

        // Display Neccessary Materials
        for (int i = 0; i < materialSlotList.Count; i++)
        {
            var currMaterial = materialSlotList[i].GetComponent<MaterialSlotData>();
            if (i >= recipe.Materials.Count)
            {
                currMaterial.gameObject.SetActive(false);
                continue;
            }

            currMaterial.gameObject.SetActive(true);
            currMaterial.SetContentData(recipe.Materials[i], multiplier);
        }
    }
    public void UpdateDescriptionUI(RecipeSO currRecipe, int amountToCraft)
    {
        // Fill materials UI
        FillDescriptionUI(currRecipe, amountToCraft);

        // Update the amount to craft and craft time UI elements
        string formattedTime = FormatCraftingTime(currRecipe.CraftingTimeInSeconds * amountToCraft);
        craftAmountText.text = amountToCraft.ToString();
        craftTimeText.text = formattedTime;
    }
    private string FormatCraftingTime(int time)
    {
        int h = TimeSpan.FromSeconds(time).Hours;
        int m = TimeSpan.FromSeconds(time).Minutes;
        int s = TimeSpan.FromSeconds(time).Seconds;

        return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
    }

    public void CraftButton()
    {
        currentStation.AddToCraftingQueue();
    }
}
