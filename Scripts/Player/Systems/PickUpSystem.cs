using Player;
using Player.interactable;
using System_UI;
using Unity.VisualScripting;
using UnityEngine;
using World;
using World.Items;
using Timer = System.Timers.Timer;

public class PickUpSystem : MonoBehaviour
{

    void Start()
    {
        InteractableSystem.OnHoverOnInteractableItem += HoverOnInteractableItemEvent;
    }

    private void HoverOnInteractableItemEvent(ItemStack eventStack, int eventObjectUuid)
    {
        if (!Input.GetKeyUp(KeyCode.F)) return;

        if (PlayerManager.Inventory.CanHoldItem(eventStack))
        {
            WorldManager.DeleteItem(eventObjectUuid);
            PlayerManager.Inventory.AddItem(eventStack);
            Debug.Log(eventStack.ToString());
            PlayerManager.Inventory.UpdateInventoryUI();
        }
        else
        {
            PlayerManager.ActionBar.SetText("Cannot hold (" + eventStack.StackSize + "x) " + eventStack.Item.name,
                ActionBar.ActionBarPriority.Highest);
            PlayerManager.ActionBar.SetTextColor(Color.red);
            PlayerManager.ActionBar.SetTimer(1);
            PlayerManager.ActionBar.Enable(true);
        }
    }


}
