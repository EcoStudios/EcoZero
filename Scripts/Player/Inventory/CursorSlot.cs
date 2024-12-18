using System;
using UnityEngine;

namespace Player.Inventory
{
    public class CursorSlot : MonoBehaviour
    {
        public Slot Slot { get; private set; }
        public new GameObject gameObject;

        void Start()
        {
            Slot = gameObject.GetComponent<Slot>();
        }

        private void Update()
        {
            if (Slot.GetCurrentStack() != null)
            {
                gameObject.transform.position = Input.mousePosition;
                gameObject.SetActive(true);
            }
            else
            {
                if (gameObject.activeInHierarchy) gameObject.SetActive(false);
            }
        }
    }
}
