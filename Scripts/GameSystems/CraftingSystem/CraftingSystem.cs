using GameSystems.CraftingSystem;
using Player;
using UnityEngine;
using World.Items;

public class CraftingSystem
{

    public CraftingSystem()
    {
        CraftingUI.OnCraftingTry += OnCraftingTry;
    }

    private void OnCraftingTry(CraftingTable craftingTable, RecipeSlot recipeSlot)
    {
        // Checking if player has all ingredients
        foreach (RecipeItem recipeItem in recipeSlot.recipe.ingredients)
        {
            if (!PlayerManager.Inventory.HasItem(recipeItem.item, recipeItem.amount)) return;
        }
        
        // Removing the ingredients from inventory
        foreach (RecipeItem recipeItem in recipeSlot.recipe.ingredients)
        {
            Item item = recipeItem.item;
            int amount = recipeItem.amount;
            
        }
    }
}
