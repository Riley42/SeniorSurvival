using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Objects/Create new Recipe")]
public class RecipeSO : ScriptableObject
{
    public string RecipeID;
    public int CraftingTimeInSeconds;
    public bool IsKnown;
    public RecipeItem CreatedItem;
    public List<RecipeItem> Materials;
}

[System.Serializable]
public class RecipeItem
{
    public ItemSO Item;
    [Range(1, 99)]
    public int AmountRequiredToCraft;
}

