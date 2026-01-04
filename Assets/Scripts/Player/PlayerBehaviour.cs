using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] ItemSO testItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlacementManager.Instance.ActivatePlacementMode(testItem);
        }
    }

    public void TryHarvest(HarvestItemSO itemUsed)
    {
        IHarvestable target = SelectionManager.Instance.CheckForHarvestable();

        if (target != null)
        {
            target.HarvestResource(itemUsed);
        }
    }
}
