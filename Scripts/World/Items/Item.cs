using JetBrains.Annotations;
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
        public ItemType itemType;

        
        public enum ItemType
        { 
           Null,
           Structure,
           Weapon,
           Armor, 
           Food,
           Material
        }


    }
    

}
