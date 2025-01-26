using System;
using UnityEngine;
using World;
using World.Items;

namespace Player.Systems
{
    public class InteractableSystem : MonoBehaviour
    {

        public static event Action<ItemStack, Guid> OnHoverOnInteractableItem;
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
            if (eventObject.transform.parent.gameObject != WorldManager.droppedItemsFolder) return;
            
            Guid guid = Guid.Parse(eventObject.name);
            if (!WorldManager.ItemExistsInWorld(guid)) return;
            
            ItemStack stack = WorldManager.GetItemStack(guid);
            if (stack == null || !stack.Item.isInteractable) return;
            
            OnHoverOnInteractableItem?.Invoke(stack, guid);
        }
        
        

    }
}
