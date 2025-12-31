using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{


    public void TryHarvest(HarvestItemSO itemUsed)
    {
        IHarvestable target = SelectionManager.Instance.CheckForHarvestable();

        if (target != null)
        {
            target.HarvestResource(itemUsed);
        }
    }
}
