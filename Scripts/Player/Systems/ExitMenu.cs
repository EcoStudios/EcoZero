using UnityEngine;
using UnityEngine.UI;

namespace Player.Systems
{
    public class ExitMenu
    {

        private readonly GameObject _exitMenu;
    
        public ExitMenu()
        {
            GameObject exitMenuPrefab = Resources.Load("Ui Prefabs/Menus/ExitMenu") as GameObject;
            _exitMenu = Object.Instantiate(exitMenuPrefab);
            
            Button[] exitButton = _exitMenu.GetComponentsInChildren<Button>();

            foreach (Button btn in exitButton)
            {
                if (btn.name == "Return")
                {
                    btn.onClick.AddListener(ReturnToGame);
                }
                else
                {
                    btn.onClick.AddListener(ReturnToMainMenu);
                }
            }
        }

        public void DeleteObject()
        {
            Object.Destroy(_exitMenu);
        }

        private void ReturnToGame()
        {
            GameManager.DisableCursor();
            GameManager.ActiveGameState = GameManager.GameState.Playing;
            DeleteObject();
        }

        private void ReturnToMainMenu()
        {
            GameManager.Instance.LoadToMainMenu();
            DeleteObject();
        }

    }
}
