using System.Collections.Generic;
using UnityEngine;
using World.Items;
using Random = UnityEngine.Random;

namespace World
{
    public class WorldManager : MonoBehaviour
    {

        public Item testItem;

        private static GameObject _droppedItemsFolder;

        private static readonly Dictionary<int, ItemStack> DroppedItems = new();

        public static void SpawnItem(ItemStack itemStack, Vector3 position)
        {
            GameObject itemObject = Instantiate(itemStack.Item.itemPrefab, _droppedItemsFolder.transform, true);
            itemObject.transform.position = position;
            itemObject.transform.rotation = Quaternion.identity;

            int uuid = CreateUuid();
            itemObject.name = uuid.ToString();
            
            DroppedItems.Add(uuid, itemStack);
        }

        public static void DeleteItem(int uuid)
        {
            Transform iTransform = _droppedItemsFolder.transform.Find(uuid.ToString());
            Destroy(iTransform.gameObject);
            DroppedItems.Remove(uuid);
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


        void Start()
        {
            _droppedItemsFolder = new GameObject("DroppedItemsFolder");
            SpawnItem(new ItemStack(testItem, 5), new Vector3(5, 0, 5));
        }
    }
}
