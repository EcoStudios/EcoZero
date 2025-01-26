using System;
using System_UI;
using UnityEngine;
using World.Items;
using Random = UnityEngine.Random;

namespace Player.Systems
{
    public class HoverInfoSystem : MonoBehaviour
    {

        private int _currentRandom;
        private int _previousRandom;
        

        void Start()
        {
            InteractableSystem.OnHoverOnInteractableItem += OnHoverOnInteractableItem;
        }

        void Update()
        {
            if (_currentRandom == -404) return;
            if (_currentRandom == _previousRandom)
            {
                if (PlayerManager.ActionBar.currentPriority > ActionBar.ActionBarPriority.Medium) return;
                PlayerManager.ActionBar.Disable();
                _currentRandom = -404;
            }
            else
            {
                _previousRandom = _currentRandom;
            }
        }

        private void OnHoverOnInteractableItem(ItemStack eventStack, Guid eventObjectGuid)
        {
            if (PlayerManager.ActionBar.currentPriority > ActionBar.ActionBarPriority.Medium) return;
            string text = "Press [F] to pick up:\n(" + eventStack.StackSize + "X) " + eventStack.Item.name;
            PlayerManager.ActionBar.SetText(text, ActionBar.ActionBarPriority.Medium);
            PlayerManager.ActionBar.SetTextColor(Color.white); 
            _ = PlayerManager.ActionBar.Enable();
            _currentRandom = Random.Range(1, 1000);
        }

    }
}
