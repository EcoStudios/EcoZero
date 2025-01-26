using Player;
using System_UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameSystems.CraftingSystem
{
    public class CraftingTable : MonoBehaviour
    {
        
        private int _currentRandom;
        private int _previousRandom;

        public CraftingMenu CraftingMenu { get; private set; }
        public bool IsOpened { get; private set; }
        public GameObject TableGameObject { get; private set; }

        void Start()
        {
            PlayerManager.Selector.OnHoverOnObject += SelectorOnOnHoverOnObject;
            CraftingMenu = new CraftingMenu(RecipeManager.GetAllRecipes());
            
            TableGameObject = gameObject;
        }
        

        void Update()
        {
            // Closing menu
            if (IsOpened && Input.GetKeyUp(KeyCode.Escape))
            {
                IsOpened = false;
                CraftingMenu.CloseMenu();
                gameObject.GetComponent<CraftingUI>().ResetCraftingTable();
                PlayerManager.Selector.Enable();
                GameManager.DisableCursor();
                GameManager.activeGameState = GameManager.GameState.Playing;
            }

            // Actionbar shit
            if (_currentRandom == -404) return;
            if (_currentRandom == _previousRandom)
            {
                if (PlayerManager.ActionBar.currentPriority > ActionBar.ActionBarPriority.Medium) return;
                PlayerManager.ActionBar.Disable();
                _currentRandom = -404;
            }
            else
            {
                _previousRandom = _currentRandom;
            }
        }

        
        // When player hovers over teh game obj
        private void SelectorOnOnHoverOnObject(GameObject obj)
        {
            if (obj == gameObject && GameManager.activeGameState == GameManager.GameState.Playing)
            {
                // Opening the crafting table
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GameManager.activeGameState = GameManager.GameState.ActiveInGameUI;
                    IsOpened = true;
                    PlayerManager.ActionBar.Disable();
                    PlayerManager.Selector.Disable();
                    GameManager.EnableCursor();
                    CraftingMenu.OpenMenu();
                    return;
                }
                // just hovering over
                string text = "Press [F] to Open";
                PlayerManager.ActionBar.SetText(text, ActionBar.ActionBarPriority.Medium);
                PlayerManager.ActionBar.SetTextColor(Color.white); 
                _ = PlayerManager.ActionBar.Enable();
                _currentRandom = Random.Range(1, 1000);
            }
        }
    }
}
