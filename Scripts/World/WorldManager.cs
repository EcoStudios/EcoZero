using System.Collections.Generic;
using UnityEngine;
using World.Items;
using Random = UnityEngine.Random;

namespace World
{
    public class WorldManager : MonoBehaviour
    {

        public Item testItem;

        public static GameObject DroppedItemsFolder;

        private static readonly Dictionary<int, ItemStack> DroppedItems = new();

        public static void SpawnItem(ItemStack itemStack, Vector3 position)
        {
            GameObject itemObject = Instantiate(itemStack.Item.itemPrefab, DroppedItemsFolder.transform, true);
            itemObject.transform.position = position;
            itemObject.transform.rotation = Quaternion.identity;

            int uuid = CreateUuid();
            itemObject.name = uuid.ToString();
            
            DroppedItems.Add(uuid, itemStack);
        }

        public static void DeleteItem(int uuid)
        {
            Transform iTransform = DroppedItemsFolder.transform.Find(uuid.ToString());
            Destroy(iTransform.gameObject);
            DroppedItems.Remove(uuid);
        }

        public static ItemStack GetItemStack(int uuid)
        {
            return DroppedItems[uuid];
        }

        private static int CreateUuid()
        {
            int uuid = Random.Range(1000000, 10000000);
            while (DroppedItems.ContainsKey(uuid))
            {
                uuid = Random.Range(1000000, 10000000);
            }
            return uuid;
        }

        public static bool ItemExistsInWorld(int uuid) => DroppedItems.ContainsKey(uuid);


        void Start()
        {
            DroppedItemsFolder = new GameObject("DroppedItemsFolder");
            SpawnItem(new ItemStack(testItem, 5), new Vector3(5, 0, 5));
        }
    }
}
