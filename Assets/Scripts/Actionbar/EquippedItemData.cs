using UnityEngine;

public class EquippedItemData : MonoBehaviour
{
    [SerializeField] ItemSO item;
    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ActionbarManager.OnUse += UseEquippedItem;
    }
    void OnDisable()
    {
        ActionbarManager.OnUse -= UseEquippedItem;
    }

    public void UseEquippedItem(ItemSO usedItem)
    {
        if (usedItem != item) return;

        if (anim != null)
        {
            anim.Play(item.AnimString);
        }
    }

    public void PerformHarvestingAnimationEvent()
    {
        var behaviour = GetComponentInParent<PlayerBehaviour>();
        if (behaviour != null && item is HarvestItemSO harvestItem)
        {
            behaviour.TryHarvest(harvestItem);
        }
    }
}
