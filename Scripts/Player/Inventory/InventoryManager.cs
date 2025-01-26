using System;
using System_UI;
using UnityEngine;
using World;
using World.Items;

namespace Player.Inventory
{
    public class InventoryManager : MonoBehaviour
    {

        public InventoryUI InventoryUISystem { get; private set; }
        public CursorSlot CursorSlotSystem { get; private set; }
        public DragAndDrop DragAndDropSystem { get; private set; }

        public int maxSlots = 36;

        private ItemStack[] _contents = new ItemStack[36];

        void Start()
        {
            _contents = new ItemStack[maxSlots];
            InventoryUISystem = PlayerManager.InventoryObject.GetComponent<InventoryUI>();
            CursorSlotSystem = PlayerManager.InventoryObject.GetComponentInChildren<CursorSlot>();
            DragAndDropSystem = PlayerManager.InventoryObject.GetComponentInChildren<DragAndDrop>();
            UpdateInventoryUI();
        }

        public ItemStack GetItem(int slot)
        {
            return _contents[slot];
        }

        public void SetSlot(int slot, ItemStack item)
        {
            _contents[slot] = item;
            UpdateInventoryUI();
        }

        public bool CanHoldItem(ItemStack item)
        {
            if (GetEmptySlot() != -404) return true;
            if (FindItem(item.Item) != -404)
            {
                ItemStack stack = _contents[FindItem(item.Item)];
                if (stack.StackSize + item.StackSize <= stack.Item.maxStackSize) return true;
            }
            return false;
        }

        public int AddItem(ItemStack item)
        {
            int slot;
            // Checks if there's an existing item that has enough room for the item.
            for (int i = 0; i < _contents.Length; i++)
            {
                ItemStack currentItem = _contents[i];
                if (currentItem == null) continue;
                if (currentItem.Item == item.Item && currentItem.Item.maxStackSize >= item.StackSize + currentItem.StackSize)
                {
                    slot = i;
                    currentItem.AddAmount(item.StackSize);
                    return slot;
                }
            }
            // Gets an empty slot if it cannot find an existing item with room.
            slot = GetEmptySlot();
            if (slot == -404)
            {
                Console.Error.WriteLine("Can't find empty slot!");
                return slot;
            }
            SetSlot(slot, item);
            if (PlayerManager.InventoryObject.activeInHierarchy) UpdateInventoryUI();
            return slot;
        }

        public int RemoveItem(ItemStack item, int slot = -404)
        {
            if (slot == -404)
            {
                slot = FindItem(item.Item);
                if (slot == -404)
                {
                    Console.Error.WriteLine("Can't find that Item!");
                    return slot;
                }
            }
        
            int byProduct = _contents[slot].StackSize - item.StackSize;
            if (byProduct <= 0)
            {
                ClearSlot(slot);
                return slot;
            }
        
            _contents[slot].RemoveAmount(item.StackSize);
            if (PlayerManager.InventoryObject.activeInHierarchy) UpdateInventoryUI();
            return slot;
        }

        private int GetEmptySlot()
        {
            int result = -404;
            for (int i = 0; i < _contents.Length; i++)
            {
                if (_contents[i] == null)
                {
                    result = i;
                }
            }
            return result;
        }

        public int FindItem(Item item)
        {
            int result = -404;
            for (int i = 0; i < _contents.Length; i++)
            {
                ItemStack currentItem = _contents[i];
                if (currentItem == null) continue;
                if (item.itemName == currentItem.Item.itemName)
                {
                    Debug.Log("yes");
                    result = i;
                }
            }
            return result;
        }

        public bool HasItem(Item item, int amount = 0)
        {
            bool result = FindItem(item) != -404;
            if (amount != 0)
            {
                for (int i = 0; i < _contents.Length; i++)
                {
                    ItemStack currentItem = _contents[i];
                    if (currentItem == null) continue;
                    if (item.itemName == currentItem.Item.itemName && currentItem.StackSize >= amount)
                    {
                        Debug.Log("yes");
                        result = true;
                    }
                }
            }

            return result;
        }

        public void ClearSlot(int slot)
        {
            _contents[slot] = null;
            InventoryUISystem.GetSlot(slot).ClearSlot();
        }

        public void ClearInventory()
        {
            _contents = new ItemStack[maxSlots];
        }

        public void OpenInventory()
        {
            PlayerManager.InventoryObject.SetActive(true);
            PlayerManager.Selector.Disable();
            GameManager.EnableCursor();
            UpdateInventoryUI();
        }

        public void UpdateInventoryUI()
        {
            for (int i = 0; i < _contents.Length; i++)
            {
                if (_contents[i] == null) continue;
                if (InventoryUISystem.Slots == null || i >= InventoryUISystem.Slots.Length) break;
                Slot slot = InventoryUISystem.GetSlot(i);
                if (!slot)
                {
                    Console.Error.WriteLine("Can't get slot: " + i);
                    break;
                }
                    
                slot.SetSlot(_contents[i]);
            }
        }

        public void CloseInventory()
        {
            PlayerManager.InventoryObject.SetActive(false);
            GameManager.DisableCursor();
            PlayerManager.Selector.Enable();
            if (DragAndDropSystem && DragAndDropSystem.IsDragging)
            {
                // Drops the item in cursor slot when inv close
                if (CursorSlotSystem.Slot.GetCurrentStack() != null)
                {
                    WorldManager.SpawnItem(CursorSlotSystem.Slot.GetCurrentStack(), PlayerManager.PlayerGameObj.transform.forward, true);
                }
                DragAndDropSystem.Reset();
            }
        }




    }
}
