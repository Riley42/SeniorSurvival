using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemData;

    public string GetDisplayName()
    {
        return itemData.DisplayName;
    }
    public void Interact()
    {
        // Add the item to the player's inventory
        Destroy(gameObject);
    }
}
