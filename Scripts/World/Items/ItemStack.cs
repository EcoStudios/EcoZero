using UnityEngine;

namespace World.Items
{
    public class ItemStack
    {

        public Item Item { get; private set; }
        public int StackSize { get; private set; }

        public ItemStack(Item item, int amount)
        {
            Item = item;
            StackSize = amount;
        }

        public void AddAmount(int amount)
        {
            StackSize = Mathf.Clamp(amount + StackSize, 1, Item.maxStackSize);
        }

        public void RemoveAmount(int amount)
        {
            StackSize -= amount;
        }

        public void SetAmount(int amount)
        {
            StackSize = amount;
        }

        public new string ToString()
        {
            return "\n" + Item.itemName + ": \n" + "  - Stack Size: " + StackSize + "/" + Item.maxStackSize + " \n";
        }

    }
}
