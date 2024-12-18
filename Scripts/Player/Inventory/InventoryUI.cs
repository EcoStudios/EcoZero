using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.Inventory
{
    public class InventoryUI : MonoBehaviour
    {

        public Slot[] Slots { get; private set; }
        
        void Start()
        {
            Slots = GetComponentsInChildren<Slot>();
        }

        public Slot GetSlot(int index)
        {
            return Slots[index];
        }
        




    }
}
