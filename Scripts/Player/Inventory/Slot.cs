using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World.Items;

namespace Player.Inventory
{
    public class Slot : MonoBehaviour
    {
        private ItemStack _itemStack;
    
        [SerializeField] private int slotNumber;

        private Image _image;
        private TMP_Text _text;

        void Start()
        {
            Image[] images = GetComponentsInChildren<Image>(true);
            foreach (Image image in images)
            {
                if (image.name == "Icon")
                {
                    _image = image;
                    break;
                }
            }

            _text = GetComponentInChildren<TMP_Text>(true);
        }

        public void SetSlot(ItemStack itemStack)
        {
            _itemStack = itemStack;
            UpdateProperties();
        
        }

        public ItemStack GetCurrentStack()
        {
            return _itemStack;
        }
    
        public void ClearSlot()
        {
            _itemStack = null;
            UpdateProperties();
        }


        private int _iteration;
        
        private void UpdateProperties()
        {
            if (_itemStack == null)
            {
                
                try
                {
                    _image.transform.parent.gameObject.SetActive(false);
                }
                catch (NullReferenceException) { } 
                return;
            }

            try
            {
                _image.transform.parent.gameObject.SetActive(true);
                _image.sprite = _itemStack.Item.itemIcon;
                _text.text = _itemStack.StackSize.ToString();
                _text.color = _itemStack.StackSize >= _itemStack.Item.maxStackSize ? Color.red : Color.white;
            }
            catch (NullReferenceException) { }
        }

        private void Update()
        {
            if (PlayerManager.InventoryObject.activeInHierarchy)
            {
                UpdateProperties();
            }
        }

        public int GetSlotNumber()
        {
            return slotNumber;
        }
    }
}
