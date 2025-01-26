using System;
using Player;
using Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World.Items;

namespace GameSystems.CraftingSystem
{
    public class CraftingUI : MonoBehaviour
    {

        public static event Action<CraftingTable, RecipeSlot> OnCraftingTry;
    
        // table scripts
        private CraftingTable _craftingTable;
        private RecipeSelection _recipeSelection;
        
        // Ui elem
        private Button _craftButton;
        private Image _recipeIcon;
        private TMP_Text _recipeName;
        private TMP_Text _recipeAmount;
        private GameObject _ingredientContainer;
        
        // other shitz
        private RecipeSlot _currentSelectedRecipeSlot;
        private GameObject _recipeCraftGameObject;

        void Start()
        {
            _craftingTable = transform.GetComponent<CraftingTable>();
            _recipeSelection = transform.GetComponent<RecipeSelection>();
            _recipeSelection.OnRecipeSelected += OnRecipeSelected;
        }

        void Update() 
        {
            if (!_craftingTable.IsOpened || _craftButton) return;
            
            _recipeCraftGameObject = 
                _craftingTable.CraftingMenu.CraftingTableMenu.transform.Find("RecipeCraft").gameObject;
            
            _craftButton = _recipeCraftGameObject.GetComponentInChildren<Button>();
            _recipeIcon = _recipeCraftGameObject.transform.Find("Panel").Find("ItemInfo").GetComponentInChildren<Image>();
            _recipeAmount = _recipeCraftGameObject.transform.Find("Panel").Find("ItemInfo").GetComponentInChildren<TMP_Text>();
            _recipeName = _recipeCraftGameObject.transform.Find("Panel").Find("ItemName").GetComponent<TMP_Text>();
            _ingredientContainer = _recipeCraftGameObject.transform.Find("Panel").Find("IngredientContainer").gameObject;
            
            _craftButton.onClick.AddListener(OnCraftButtonClicked);
            
        }

        public void ResetCraftingTable()
        {
            _craftingTable.CraftingMenu.CraftingTableMenu.SetActive(false);
            _craftingTable.CraftingMenu.CraftingTableMenu.transform.localPosition = Vector3.zero;
            _craftingTable.CraftingMenu.CraftingTableMenu.SetActive(true);
            _recipeCraftGameObject.SetActive(false);
        }

        private void OnRecipeSelected(RecipeSlot recipeSlot)
        {
            _craftingTable.CraftingMenu.CraftingTableMenu.SetActive(false);
            _craftingTable.CraftingMenu.CraftingTableMenu.transform.localPosition =
                new Vector3(-200, _craftingTable.CraftingMenu.CraftingTableMenu.transform.localPosition.y, 0f);
            _craftingTable.CraftingMenu.CraftingTableMenu.SetActive(true);
            _recipeCraftGameObject.SetActive(true);
            _currentSelectedRecipeSlot = recipeSlot;
            
            SetInformation(recipeSlot);
        }

        private void SetInformation(RecipeSlot recipeSlot)
        {
            _recipeAmount.text = recipeSlot.recipe.amount.ToString();
            _recipeIcon.sprite = recipeSlot.recipe.item.itemIcon;
            _recipeName.text = recipeSlot.recipe.item.name;
            SetIngredients(recipeSlot.recipe.ingredients);
        }

        private void SetIngredients(RecipeItem[] ingredients)
        {
            GameObject panel = _ingredientContainer.transform.Find("Panel").gameObject;
            for (var i = 0; i < 4; i++)
            {
                if (i >= ingredients.Length) break;
                int slot = PlayerManager.Inventory.FindItem(ingredients[i].item);
                Debug.Log(slot);
                int amountInInv = 0;
                if (slot != -404) amountInInv = PlayerManager.Inventory.GetItem(slot).StackSize;
                
                GameObject currentSlot = panel.transform.GetChild(i).gameObject;
                currentSlot.SetActive(true);
                
                foreach (TMP_Text txt in currentSlot.GetComponentsInChildren<TMP_Text>())
                {
                    if (txt.name == "Size") txt.text = amountInInv + "/" + ingredients[i].amount;
                }
                foreach (Image img in currentSlot.GetComponentsInChildren<Image>())
                {
                    if (img.name == "Icon") img.sprite = ingredients[i].item.itemIcon;
                }
                
            }
            
            // Setting the rest of the ingredient slots to not active.
            if (ingredients.Length < 4)
            {
                for (int i = 0; i < 4-ingredients.Length; i++)
                {
                    GameObject currentSlot = panel.transform.GetChild(ingredients.Length+i).gameObject;
                    currentSlot.SetActive(false);
                }
            }
        }

        private void OnCraftButtonClicked()
        {
            if (!_currentSelectedRecipeSlot) return;
            Debug.Log("a");
            OnCraftingTry?.Invoke(_craftingTable, _currentSelectedRecipeSlot);
        }





    }
}
