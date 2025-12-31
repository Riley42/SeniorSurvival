using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] InventoryItem content;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI quantityText;

    public InventoryItem Content => content;


    public void SetSlotData(InventoryItem newItem)
    {
        // Reset the slot if the new item is null
        if (newItem.Item == null)
        {
            ResetSlotData();
            return;
        }

        // Assign the new item to the slot
        content = newItem;
        icon.gameObject.SetActive(true);
        icon.sprite = content.Item.Icon;

        // Only show the quantity text if the item has a stack amount greater than 1
        if (content.CurrentStackAmount > 1)
        {
            quantityText.transform.parent.gameObject.SetActive(true);
            quantityText.text = content.CurrentStackAmount.ToString();
        }
        else
        {
            quantityText.transform.parent.gameObject.SetActive(false);
            quantityText.text = string.Empty;
        }
    }

    public void ResetSlotData()
    {
        content = new InventoryItem();
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;

        // Deactivate the quantity background, not just the text
        quantityText.transform.parent.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        SetSlotData(content);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        var dragged = DragManager.Instance.DraggedItem;
        var source = DragManager.Instance.SourceSlot;

        if (dragged != null && source != this)
        {
            // Swap items logic
            var temp = this.Content;
            this.SetSlotData(dragged);
            source.SetSlotData(temp);
        }
    }
}
