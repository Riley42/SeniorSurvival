using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    int defaultSlotAmount = 24;
    [SerializeField] GameObject inventoryWindow;
    [SerializeField] Transform slotContainer;
    [SerializeField] Slot slotPrefab;
    [SerializeField] List<Slot> inventorySlotList = new List<Slot>();

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += ResetInventorySlots;
        UIInputManager.Instance.OnInventoryToggled += ToggleWindow;

        for (int i = 0; i < defaultSlotAmount; i++)
        {
            var newSlot = Instantiate(slotPrefab, slotContainer);
            inventorySlotList.Add(newSlot);
        }
        ResetInventorySlots();
    }

    void ToggleWindow(bool isOpen)
    {
        print("Toggling Inventory UI: " + isOpen);
        if (isOpen)
        {
            inventoryWindow.SetActive(true);
            ResetInventorySlots();
        }
        else
        {
            inventoryWindow.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= ResetInventorySlots;

        if (UIInputManager.Instance != null)
            UIInputManager.Instance.OnInventoryToggled -= ToggleWindow;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ResetInventorySlots();
        }
    }

    void FillInventory()
    {
        var inventory = InventoryManager.Instance.ItemsInInventory;

        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItem currentItem = inventory[i];

            if (currentItem.CurrentStackAmount > 0)
            {
                inventorySlotList[i].SetSlotData(currentItem);
            }
        }
    }

    public void ResetInventorySlots()
    {
        foreach (Slot slot in inventorySlotList)
        {
            slot.ResetSlotData();
        }

        FillInventory();
    }
}
