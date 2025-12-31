using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : Slot, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (Content != null && Content.CurrentStackAmount > 0)
        {
            DragManager.Instance.StartDrag(Content, this);
        }
    }

    public override void OnDrag(PointerEventData eventData) { }

    public override void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.EndDrag();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        /*var dragged = DragManager.Instance.DraggedItem;
        var source = DragManager.Instance.SourceSlot;

        if (dragged != null && source != this)
        {
            // Swap items logic
            var temp = this.Content;
            this.SetSlotData(dragged);
            source.SetSlotData(temp);
        }*/
    }
}
