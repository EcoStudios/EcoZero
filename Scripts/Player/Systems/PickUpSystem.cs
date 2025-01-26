using System;
using System_UI;
using UnityEngine;
using World;
using World.Items;

namespace Player.Systems
{
    public class PickUpSystem : MonoBehaviour
    {

        void Start()
        {
            InteractableSystem.OnHoverOnInteractableItem += HoverOnInteractableItemEvent;
        }

        private void HoverOnInteractableItemEvent(ItemStack eventStack, Guid eventObjectGuid)
        {
            if (!Input.GetKeyUp(KeyCode.F)) return;

            if (PlayerManager.Inventory.CanHoldItem(eventStack))
            {
                WorldManager.DeleteItem(eventObjectGuid);
                PlayerManager.Inventory.AddItem(eventStack);
                Debug.Log(eventStack.ToString());
                PlayerManager.Inventory.UpdateInventoryUI();
            }
            else
            {
                PlayerManager.ActionBar.SetText("Cannot hold (" + eventStack.StackSize + "x) " + eventStack.Item.name, ActionBar.ActionBarPriority.Highest);
                PlayerManager.ActionBar.SetTextColor(Color.red);
                PlayerManager.ActionBar.SetTimer(1);
                _ = PlayerManager.ActionBar.Enable(true);
            }
        }


    }
}
