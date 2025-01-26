using System;
using UnityEngine;
using World.Items;

namespace GameSystems.CraftingSystem
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "CraftingSystem/Recipe")]
    public class Recipe : ScriptableObject
    {

        public Item item;
        public int amount;
        
        public RecipeItem[] ingredients;
    }

    [Serializable]
    public class RecipeItem
    {
        public Item item;
        public int amount;

        public RecipeItem(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }

    public class RecipeManager
    {
        
        private static Recipe[] _allRecipes;

        public static Recipe[] GetAllRecipes()
        {
            return _allRecipes ??= Resources.LoadAll<Recipe>("RecipesSO");
        }

    }
}
