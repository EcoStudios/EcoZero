using System;
using UnityEngine;
using World;
using World.Items;

namespace Player.interactable
{
    public class InteractableSystem : MonoBehaviour
    {

        public static event Action<ItemStack, int> OnHoverOnInteractableItem;
        private bool _isSet;

        void Update()
        {
            if (!_isSet)
            {
                try
                {
                    PlayerManager.Selector.OnHoverOnObject += HoverEvent;
                    _isSet = true;
                }
                catch (NullReferenceException) { }
            }
        }

        private void HoverEvent(GameObject eventObject)
        {
            if (eventObject.transform.parent.gameObject != WorldManager.DroppedItemsFolder) return;
            
            int uuid = Int32.Parse(eventObject.name);
            if (!WorldManager.ItemExistsInWorld(uuid)) return;
            
            ItemStack stack = WorldManager.GetItemStack(uuid);
            if (stack == null || !stack.Item.isInteractable) return;
            
            Debug.Log(stack.ToString());
            
            OnHoverOnInteractableItem?.Invoke(stack, uuid);
        }
        
        

    }
}
