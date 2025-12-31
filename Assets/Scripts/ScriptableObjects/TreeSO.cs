using UnityEngine;

[CreateAssetMenu(fileName = "TreeSO", menuName = "Scriptable Objects/TreeSO")]
public class TreeSO : ScriptableObject
{
    [Header("Basic Info")]
    public string DisplayName;
    public float MaxHealth = 100f;

    [Header("Harvesting Info")]
    public ItemSO ResourceDrop;
    public int BaseYieldPerHit = 1;
    public int OptimalYieldPerHit = 4;
    public ToolType OptimalTool;
    //public AudioClip ChopSound;

}
