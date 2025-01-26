using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.CraftingSystem
{
    
    
    public class CraftingMenu
    {
        // Prefabs
        private readonly GameObject _recipeSlotPrefab = Resources.Load<GameObject>("Ui Prefabs/CraftingSystem/RecipeSlot");
        private readonly GameObject _craftingTablePrefab = Resources.Load<GameObject>("Ui Prefabs/CraftingSystem/CraftingTable");
        public GameObject ParentGameObject;
        public GameObject CraftingTableMenu;
        
        // Other vars
        private readonly Recipe[] _recipes;
        private bool _slotSet;

        public CraftingMenu(Recipe[] recipes)
        {
            _recipes = recipes;
            CreateGameObject();
        }

        private void CreateGameObject()
        {
            // Creating parent/Canvas
            ParentGameObject = new GameObject("CraftingMenu");
            ParentGameObject.SetActive(false);
                // Creating Canvas
            ParentGameObject.AddComponent<Canvas>();
            ParentGameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            ParentGameObject.AddComponent<CanvasScaler>();
            ParentGameObject.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            ParentGameObject.AddComponent<GraphicRaycaster>();
            
            // Creating CraftingTable Menu
            CraftingTableMenu = Object.Instantiate(_craftingTablePrefab, ParentGameObject.transform);
        }

        public void CloseMenu()
        {
            ParentGameObject.SetActive(false);
        }

        public void OpenMenu()
        {
            // Opening
            ParentGameObject.SetActive(true);
            // Setting up menu
            if (!_slotSet)
            {
                int column = 1;
                int row = 1;
                for (var i = 1; i <= _recipes.Length; i++)
                {
                    if (column > 5)
                    {
                        row += 1;
                        column = 1;
                    }
                    
                    int slotIndex = i;
                    float posX = Mathf.Clamp(-533 + 177f * column, -533, 1622);
                    float posY = Mathf.Clamp(340 - 100f * row, -1000, 340);
                    Vector3 pos = new Vector3(posX, posY, 0);
                    CreateRecipeSlot(_recipes[i-1], slotIndex, pos);
                    
                    column++;
                }
                _slotSet = true;
            }
        }

        private void CreateRecipeSlot(Recipe recipe, int slotIndex, Vector3 position)
        {
            // Basic object creation
            GameObject obj = Object.Instantiate(_recipeSlotPrefab, 
                CraftingTableMenu.transform.Find("RecipeList").Find("Panel"));
            obj.AddComponent(typeof(RecipeSlot));
            obj.GetComponent<RecipeSlot>().recipe = recipe;
            obj.GetComponent<RecipeSlot>().slotIndex = slotIndex;
            
            // Setting pos
            obj.transform.localPosition = position;
            obj.transform.localScale = new Vector3(1, 0.8f, 1);
            
            // Setting raycasting shit
            obj.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            
            // Setting recipe shit
            foreach (TMP_Text txt in obj.GetComponentsInChildren<TMP_Text>())
            {
                if (txt.name == "Size") txt.text = recipe.amount.ToString();
            }
            foreach (Image img in obj.GetComponentsInChildren<Image>())
            {
                if (img.name == "Icon") img.sprite = recipe.item.itemIcon;
            }
        }





    }



    public class RecipeSlot : MonoBehaviour
    {
        
        public Recipe recipe;
        public int slotIndex;

        public RecipeSlot(Recipe recipe, int slotIndex)
        {
            this.recipe = recipe;
            this.slotIndex = slotIndex;
        }
        
    }
}
