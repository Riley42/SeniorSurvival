using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Items/Base Item")]

public class ItemSO : ScriptableObject
{
    public string ItemID;
    public string DisplayName;
    public Sprite Icon;
    public bool IsConstructable; // If true, this item can be used for construction
    public bool IsPlaceable; // If true, this item can be used placed
    public bool IsUsable; 
    public int MaxStackAmount = 10;
    public GameObject ItemPrefab;
    public string AnimString;
    public AudioClip UseSoundFX; // Sound played when using this item


    public void Use(Action onUsed = null)
    {
        Debug.Log($"Using scriptable object: {DisplayName}");
        onUsed?.Invoke();
    }
}
