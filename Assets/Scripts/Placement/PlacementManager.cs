using UnityEngine;

public class PlacementManager : Singleton<PlacementManager>
{
    [SerializeField] Transform enviromentPlaceables;
    [SerializeField] Transform placementHolder;
    bool inPlacementMode;
    PlaceableItemBehaviour itemToBePlaced;

    public bool InPlacementMode => inPlacementMode;
    public void ActivatePlacementMode(ItemSO itemToPlace)
    {
        print($"Activating placement mode for item: {itemToPlace.DisplayName}");
        GameObject item = Instantiate(itemToPlace.ItemPrefab, placementHolder);

        // Changing the name of the gameobject so it will not be (clone)
        item.name = itemToPlace.DisplayName;

        // Set layer to Ignore Raycast to prevent raycasting issues
        item.layer = LayerMask.NameToLayer("IgnorePlacementRaycast");

        // Saving a reference to the item we want to place
        itemToBePlaced = item.GetComponent<PlaceableItemBehaviour>();

        // Disabling the non-trigger collider so our mouse can cast a ray
        //itemToBePlaced.SolidCollider.enabled = false;

        // Actiavting Construction mode
        inPlacementMode = true;
    }

    void Update()
    {
        if (!inPlacementMode || itemToBePlaced == null) return;

        HandleItemPlacement();
    }

    private void HandleItemPlacement()
    {
        // Build layer mask that ignores the IgnorePlacementRaycast layer
        int layerMask = ~LayerMask.GetMask("IgnorePlacementRaycast");

        // Moving the item to be placed to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 25f, layerMask, QueryTriggerInteraction.Ignore))
        {
            itemToBePlaced.transform.position = hitInfo.point;
            itemToBePlaced.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        // Placing the item on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            PlaceItem();
        }
        // Cancelling placement mode on right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacementMode();
        }
    }

    void CancelPlacementMode()
    {
        Destroy(itemToBePlaced.gameObject);
        itemToBePlaced = null;
        inPlacementMode = false;
    }
    private void PlaceItem()
    {
        // Setting the parent to be the root of our scene
        itemToBePlaced.transform.SetParent(enviromentPlaceables.transform, true);

        // Setting the default color/material
        //itemToBePlaced.SetDefaultColor();
        //itemToBePlaced.enabled = false;

        itemToBePlaced = null;

        inPlacementMode = false;
    }
}
