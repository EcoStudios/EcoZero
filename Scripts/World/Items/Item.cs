using UnityEngine;

namespace World.Items
{
    [CreateAssetMenu(order = 0, fileName = "New Item", menuName = "New Item") ]
    public class Item : ScriptableObject
    {

        [Header("Item's Appearance")]
        public string itemName;
        public GameObject itemPrefab;
        public Sprite itemIcon;

        [Space] [Header("Item's Properties")] 
        public bool isInteractable = true;
        public float itemDamage;
        public int maxStackSize; 




    }
}
