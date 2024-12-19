using Player;
using UnityEngine;
using UnityEngine.UI;

namespace System_UI
{
    public class QuitMenu : MonoBehaviour
    {
        
        private Button _quitButton;
        private Button _returnButton;

        void Start()
        {
            _quitButton = transform.Find("Quit").GetComponent<Button>();
            _quitButton.onClick.AddListener(Quit);
            
            _returnButton = transform.Find("Return").GetComponent<Button>();
            _returnButton.onClick.AddListener(Return);
        }

        public void Quit() { PlayerManager.GameManager.QuitGame(); }

        public void Return()
        {
            PlayerManager.GameManager.gameState = GameManager.GameState.Playing;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerManager.Selector.Enable();
            Disable();
        }

        public void Disable() { gameObject.SetActive(false); }
        public void Enable() { gameObject.SetActive(true); }
    }
}
