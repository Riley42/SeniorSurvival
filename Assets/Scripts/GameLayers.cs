using UnityEngine;

public static class GameLayers
{
    public const int Player = 6;
    public const int Enemy = 7;
    public const int NPC = 8;
    public const int Interactables = 9;
    public const int Harvestable = 10;

    public static readonly LayerMask InteractableLayer = 1 << Interactables;
    public static readonly LayerMask SelectableLayer = (1 << Enemy) | (1 << NPC);
    public static readonly LayerMask HarvestableLayer = 1 << Harvestable;
}
