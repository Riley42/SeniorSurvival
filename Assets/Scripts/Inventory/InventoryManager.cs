using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] List<InventoryItem> itemsInInventory = new List<InventoryItem>();

    int currentInventorySize = 24;
    Dictionary<string, ItemSO> inventoryMap = new Dictionary<string, ItemSO>();
    public event Action OnInventoryChanged;
    public event Action OnActionBarChanged;

    public List<InventoryItem> ItemsInInventory => itemsInInventory;

    protected override void Awake()
    {
        base.Awake();
        var loadedItems = Resources.LoadAll<ItemSO>("ScriptableObjects/Items");

        if (loadedItems.Length > 0)
        {
            foreach (var item in loadedItems)
            {
                inventoryMap.Add(item.ItemID, item);
            }
        }
        for (int i = 0; i < currentInventorySize; i++)
        {
            itemsInInventory.Add(new InventoryItem());
        }
    }

    private void Update()
    {
        // Test adding items to inventory
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddItems(GetItemById("stone_axe"));
            AddItems(GetItemById("small_stone"), 99);
            AddItems(GetItemById("wood"), 99);
            AddItems(GetItemById("wild_root"), 99);
            AddItems(GetItemById("animal_fat"), 99);
            AddItems(GetItemById("clean_water"), 99);
            AddItems(GetItemById("fish"), 99);
            AddItems(GetItemById("wild_herb"), 99);
        }
    }
    public void AddItems(ItemSO itemToAdd, int amountToAdd = 1)
    {
        // This keeps track of how many ims we still need to add,
        // it'll be decremented as we stack items or fill empty slots
        int remainingToAdd = amountToAdd;

        // First, try to stack with existing items
        foreach (var currentItem in itemsInInventory)
        {
            // This checks if the current inventory item:
            //   A) Is the same item type.
            //   B) Still has room in the stack.
            if (currentItem.Item == itemToAdd && currentItem.CurrentStackAmount < itemToAdd.MaxStackAmount)
            {

                // Determine how many more items can fit in the stack.
                // Add either the full remaining amount or whatever will fit.
                // Reduce remainingToAdd accordingly.
                int roomToStack = itemToAdd.MaxStackAmount - currentItem.CurrentStackAmount;
                int addToStack = Mathf.Min(roomToStack, remainingToAdd);
                currentItem.CurrentStackAmount += addToStack;
                remainingToAdd -= addToStack;

                // If we’ve added everything, we’re done! Exit the method.
                if (remainingToAdd <= 0)
                    return;
            }
        }

        // If Items Remain, Try Empty Slots
        while (remainingToAdd > 0)
        {
            // Try to find an empty inventory slot.
            // If none are available, the inventory is full and the function exits.
            int slotToFill = FindEmptySlot();
            if (slotToFill == -1)
            {
                print("Inventory is full");
                return;
            }

            // Fill the empty slot with a new stack.
            // Add up to the item’s max stack amount or whatever remains.
            // Subtract the added amount from remainingToAdd.

            int amountToPlace = Mathf.Min(remainingToAdd, itemToAdd.MaxStackAmount);
            itemsInInventory[slotToFill] = new InventoryItem(itemToAdd, amountToPlace);
            remainingToAdd -= amountToPlace;

            // This loop continues until:
            //   A) All items are added.
            //   B) Or there are no more empty slots.
        }

        // Update UI
        OnInventoryChanged?.Invoke();

    }
    public void RemoveItems(ItemSO itemToRemove, int amountToRemove = 1)
    {
        // Before trying to remove anything, we verify that the inventory contains enough of the target item.
        // If the player doesn’t have enough, print a message and exit early.
        if (amountToRemove > GetTotalInventoryCount(itemToRemove))
        {
            print("You don't have enough items.");
            return;
        }

        // This keeps track of how many more items still need to be removed as we process inventory stacks.
        int remainingToRemove = amountToRemove;

        var existingItemStacks = itemsInInventory
            .Select((item, index) => new { item, index })   // Create an anonymous type to hold both item and index
            .Where(x => x.item.Item == itemToRemove && x.item.CurrentStackAmount > 0) // Filter for items that match and have a stack amount > 0
            .OrderBy(x => x.item.CurrentStackAmount) // Order by stack amount ascending
            .ToList(); // Convert to a list for easier indexing

        // Loop through each filtered and sorted stack until we've removed everything needed.
        foreach (var stack in existingItemStacks)
        {
            var currentItem = stack.item;

            // If the current stack is less than or equal to what we need:
            // We remove the whole stack and subtract that number from remainingToRemove.
            // Clear the slot (sets it to null or an empty InventoryItem).
            if (currentItem.CurrentStackAmount <= remainingToRemove)
            {
                remainingToRemove -= currentItem.CurrentStackAmount;
                ResetToEmptySlot(currentItem);
            }
            // Else Just subtract what we need from this stack and we’re done—exit the loop.
            else
            {
                currentItem.CurrentStackAmount -= remainingToRemove;
                break;
            }

            // Safety check to make sure we don’t continue looping unnecessarily.
            if (remainingToRemove <= 0)
                break;
        }

        // Update UI
        OnInventoryChanged?.Invoke();
        OnActionBarChanged?.Invoke(); // Notify action bar of changes, if applicable
    }
    int FindEmptySlot()
    {
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            if (itemsInInventory[i].Item == null)
            {
                return i;
            }
        }
        return -1; // No empty slots found.
    }
    void ResetToEmptySlot(InventoryItem currentItem)
    {
        currentItem.CurrentStackAmount = 0;
        currentItem.Item = null;
        currentItem.ItemId = null;
    }
    public ItemSO GetItemById(string itemId)
    {
        if (inventoryMap.TryGetValue(itemId, out ItemSO item))
        {
            return item;
        }
        return null; // Item not found
    }
    public int GetTotalInventoryCount(ItemSO item)
    {
        int total = 0;
        foreach (var i in itemsInInventory)
        {
            if (i.Item == item)
            {
                total += i.CurrentStackAmount;
            }
        }
        return total;
    }
}

[System.Serializable]
public class InventoryItem
{
    [SerializeField] ItemSO item;
    [SerializeField] string itemID;
    [SerializeField] int currentStackAmount;

    public InventoryItem()
    {
        item = null;
        itemID = null;
        currentStackAmount = 0;
    }
    public InventoryItem(ItemSO item)
    {
        this.item = item;
        itemID = item.ItemID;
        currentStackAmount = 0;
    }
    public InventoryItem(ItemSO item, int amtToPlace)
    {
        this.item = item;
        itemID = item.ItemID;
        currentStackAmount = amtToPlace;
    }

    public ItemSO Item { get { return item; } set { item = value; } }
    public string ItemId { get { return itemID; } set { itemID = value; } }
    public int CurrentStackAmount { get { return currentStackAmount; } set { currentStackAmount = value; } }
}
