using System;
using UnityEngine;
using World;
using World.Items;

namespace Player.Inventory
{
    public class DragAndDrop : MonoBehaviour
    {

        public Slot StartingSlot { get; private set; }
        public Slot EndingSlot { get; private set; }
        public bool IsDragging { get; private set; }
        public GameObject HoveredGameObject { get; private set; }
        private InventoryManager _inventory;
        

        public void Start()
        {
            _inventory = PlayerManager.Inventory;
        }

        public void Reset()
        {
            StartingSlot = null;
            EndingSlot = null;
            IsDragging = false;
            HoveredGameObject = null;
            _inventory.CursorSlotSystem.Slot.ClearSlot();
        }




        private void Update()
        {
            // Gets the current gameObject via a raycast
            if (!_inventory.InventoryUISystem.gameObject.activeInHierarchy) return;
            try
            {
                RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
                HoveredGameObject = hit.transform.gameObject;
            }
            catch (NullReferenceException)
            {
                HoveredGameObject = null;
            }

                // Dragging
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsDragging)
                {
                    IsDragging = true;
                    StartDragging();
                }
                // Dropping    
            } else if (Input.GetMouseButtonUp(0))
            {
                if (IsDragging)
                {
                    IsDragging = false;
                    StopDragging();
                    _inventory.UpdateInventoryUI();
                    _inventory.CursorSlotSystem.Slot.ClearSlot();
                    StartingSlot = null;
                    EndingSlot = null;
                }
            }

            // Right-clicking shits
            if (IsDragging && Input.GetMouseButtonDown(1)) // Right click on drag
            {
                AddToSlot();
            } 
            else if (!IsDragging && Input.GetMouseButtonDown(1)) // Right click off of drag
            {
                SpitStack();
            }
            
            

        }

        // Divides stack in half by right-clicking while not dragging
        private void SpitStack()
        {
            if (HoveredGameObject == null) return;
            Slot hoveredSlot = HoveredGameObject.GetComponent<Slot>();
            
            if (hoveredSlot.GetCurrentStack() == null || hoveredSlot.GetCurrentStack().StackSize <= 1) return;
            
            StartingSlot = hoveredSlot;
            IsDragging = true;
            
            int halfStackSize = hoveredSlot.GetCurrentStack().StackSize / 2;
            ItemStack halvedPortion = new ItemStack(hoveredSlot.GetCurrentStack().Item, halfStackSize);
            
            _inventory.CursorSlotSystem.Slot.SetSlot(halvedPortion);
            hoveredSlot.GetCurrentStack().RemoveAmount(halfStackSize);
        }

        // Adds items to a slot by right-clicking while dragging
        private void AddToSlot()
        {
            // Didn't drop onto a slot, so will drop 1 of the selected item into the world
            if (HoveredGameObject == null)
            {
                if (_inventory.CursorSlotSystem.Slot.GetCurrentStack() == null) return;
                if (_inventory.CursorSlotSystem.Slot.GetCurrentStack().StackSize - 1 <= 0)
                {
                    WorldManager.SpawnItem(_inventory.CursorSlotSystem.Slot.GetCurrentStack(),
                        PlayerManager.PlayerGameObj.transform.position + Vector3.forward * 3, true);
                    _inventory.CursorSlotSystem.Slot.ClearSlot();
                }
                else
                {
                    ItemStack droppedItem = new ItemStack(_inventory.CursorSlotSystem.Slot.GetCurrentStack().Item, 1);
                    WorldManager.SpawnItem(droppedItem, PlayerManager.PlayerGameObj.transform.position 
                                                        + Vector3.forward * 3, true);
                    _inventory.CursorSlotSystem.Slot.GetCurrentStack().RemoveAmount(1);
                }
            }
            // Dropped onto the same item so will add 1 if have room 
            else
            {
                Slot hoveredSlot = HoveredGameObject.GetComponent<Slot>();
                if (hoveredSlot == null || _inventory.CursorSlotSystem.Slot.GetCurrentStack() == null
                                        || _inventory.CursorSlotSystem.Slot.GetCurrentStack().StackSize <= 0) return;
                
                // Creates a new itemstack of the item that's in the cursor slot if the item in hovered slot is not set
                if (hoveredSlot.GetCurrentStack() == null)
                {
                    ItemStack newItemStack = new ItemStack(_inventory.CursorSlotSystem.Slot.GetCurrentStack().Item, 1);
                    _inventory.CursorSlotSystem.Slot.GetCurrentStack().RemoveAmount(newItemStack.StackSize);
                    _inventory.SetSlot(hoveredSlot.GetSlotNumber(), newItemStack);
                    _inventory.UpdateInventoryUI();
                    return;
                }
                
                // Adds one to the hovered itemstack
                if (hoveredSlot.GetCurrentStack().Item == _inventory.CursorSlotSystem.Slot.GetCurrentStack().Item)
                {
                    if (hoveredSlot.GetCurrentStack().StackSize + 1 >
                        hoveredSlot.GetCurrentStack().Item.maxStackSize) return;
                    if (_inventory.CursorSlotSystem.Slot.GetCurrentStack().StackSize - 1 <= 0)
                    {
                        hoveredSlot.GetCurrentStack().AddAmount(1);
                        _inventory.CursorSlotSystem.Slot.ClearSlot();
                    }
                    _inventory.CursorSlotSystem.Slot.GetCurrentStack().RemoveAmount(1);
                    hoveredSlot.GetCurrentStack().AddAmount(1);
                }
                
            }
        }

        private void StartDragging()
        {
            if (HoveredGameObject == null) return;
            StartingSlot = HoveredGameObject.GetComponent<Slot>();
            if (StartingSlot.GetCurrentStack() == null) return;
            try
            {
                _inventory.CursorSlotSystem.Slot.SetSlot(StartingSlot.GetCurrentStack());
            }
            catch (NullReferenceException)
            {
                _inventory.CursorSlotSystem.Slot.SetSlot(StartingSlot.GetCurrentStack());
            }
            _inventory.ClearSlot(StartingSlot.GetSlotNumber());
        }

        private void StopDragging()
        {
            if (StartingSlot == null || _inventory.CursorSlotSystem.Slot.GetCurrentStack() == null)
            {
                return;
            }
            

            // Spawns the item in cursor slot into the world when it isn't dropped onto a slot
            if (HoveredGameObject == null)
            {
                WorldManager.SpawnItem(_inventory.CursorSlotSystem.Slot.GetCurrentStack(),
                    PlayerManager.PlayerGameObj.transform.position + Vector3.forward * 3, true);
                _inventory.CursorSlotSystem.Slot.ClearSlot();
            }
            // Dropped onto a slot
            else
            {
                EndingSlot = HoveredGameObject.GetComponent<Slot>();
                if (EndingSlot.GetCurrentStack() == null) // dropped onto a slot with nothing in it
                {
                    _inventory.SetSlot(EndingSlot.GetSlotNumber(), _inventory.CursorSlotSystem.Slot.GetCurrentStack());
                    return;
                }

                ItemStack[] items = {_inventory.CursorSlotSystem.Slot.GetCurrentStack(), EndingSlot.GetCurrentStack()};
                
                // Dropped onto the same Item
                if (items[0].Item == items[1].Item)
                {
                    int totalSum =  items[0].StackSize + items[1].StackSize;
                    // Combines the items together 
                    if (totalSum <= items[0].Item.maxStackSize)
                    {
                        items[1].AddAmount(items[0].StackSize);
                        return;
                    }
                }
                
                // Switches places if the first item has a bigger stack (or equal) than the second item
                if (items[0].StackSize >= items[1].StackSize)
                {
                    _inventory.SetSlot(EndingSlot.GetSlotNumber(), items[0]);
                    _inventory.SetSlot(StartingSlot.GetSlotNumber(), items[1]);
                }
                else
                {
                    _inventory.SetSlot(StartingSlot.GetSlotNumber(), items[0]);
                }
            }
        }




    }
}
