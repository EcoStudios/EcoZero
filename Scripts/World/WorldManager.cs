using System;
using System.Collections.Generic;
using UnityEngine;
using World.Items;

namespace World
{
    public class WorldManager : MonoBehaviour
    {

        public Item testItem;
        public Item craftingTable;

        public static GameObject droppedItemsFolder;
        public static GameObject placedItemsFolder;

        private static readonly Dictionary<Guid, ItemStack> DroppedItems = new();

        public static void SpawnItem(ItemStack itemStack, Vector3 position, bool isDropped)
        {
            GameObject itemObject;
            if (isDropped)
            {
                itemObject = Instantiate(itemStack.Item.itemPrefab, droppedItemsFolder.transform, true);
            }
            else
            {
                itemObject = Instantiate(itemStack.Item.itemPrefab, placedItemsFolder.transform, true);
            }
            itemObject.transform.position = position;
            itemObject.transform.rotation = Quaternion.identity;

            Guid guid = Guid.NewGuid();
            itemObject.name = guid.ToString();
            
            DroppedItems.Add(guid, itemStack);
        }

        public static void DeleteItem(Guid guid)
        {
            Transform iTransform = droppedItemsFolder.transform.Find(guid.ToString());
            Destroy(iTransform.gameObject);
            DroppedItems.Remove(guid);
        }

        public static ItemStack GetItemStack(Guid guid)
        {
            return DroppedItems[guid];
        }
        

        public static bool ItemExistsInWorld(Guid guid) => DroppedItems.ContainsKey(guid);


        void Start()
        {
            droppedItemsFolder = new GameObject("DroppedItemsFolder");
            placedItemsFolder = new GameObject("PlacedItemsFolder");
            SpawnItem(new ItemStack(testItem, 5), new Vector3(5, 0, 5), true);
            SpawnItem(new ItemStack(craftingTable, 5), new Vector3(3, 0, 5), false);
        }
    }
}
