using UnityEngine;

public enum ItemQuality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public enum ToolType
{
    None,
    Axe,
    Pickaxe,
    Hammer
}

[CreateAssetMenu(fileName = "HarvestItemSO", menuName = "Scriptable Objects/Items/Harvest Item")]
public class HarvestItemSO : ItemSO
{
    [Header("Harvesting Properties")]
    public ItemQuality Quality = ItemQuality.Common;
    public ToolType ToolType = ToolType.None;
    public float BaseDamage = 1f; // Damage dealt to breakable objects
    public float OptimalDamage = 1f; // Damage dealt to breakable objects
}
