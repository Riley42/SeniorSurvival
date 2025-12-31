using UnityEngine;

public class DragManager : Singleton<DragManager>
{
    [SerializeField] Canvas uiCanvas;
    [SerializeField] GameObject dragGhostPrefab;
    private DragGhost currentGhost;

    public InventoryItem DraggedItem { get; private set; }
    public Slot SourceSlot { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        currentGhost = Instantiate(dragGhostPrefab, uiCanvas.transform).GetComponent<DragGhost>();
        currentGhost.Hide();
    }
    private void Update()
    {
        if (currentGhost != null)
        {
            currentGhost.transform.position = Input.mousePosition;
        }
    }

    // Inventory Dragging
    public void StartDrag(InventoryItem item, Slot sourceSlot)
    {
        DraggedItem = item;
        SourceSlot = sourceSlot;

        if (currentGhost != null)
        {
            currentGhost.Show(item.Item.Icon, item.CurrentStackAmount);
        }
    }

    public void EndDrag()
    {
        if (currentGhost != null)
        {
            currentGhost.Hide();
        }

        DraggedItem = null;
        SourceSlot = null;
    }
}
