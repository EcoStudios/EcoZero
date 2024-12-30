using UnityEngine;
using World.Items;

namespace Player.Systems.Crafting
{
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Crafting/CraftingRecipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [Header("Recipe Information")]
        public Item item;
        public int amountOfItem;
        
        public RecipeStack[] ingredients;
        
        
    }

    [System.Serializable]
    public class RecipeStack
    {
        public Item item;
        public int amountOfItem;

        public RecipeStack(Item item, int amountOfItem)
        {
            this.item = item;
            this.amountOfItem = amountOfItem;
        }
    }


}
