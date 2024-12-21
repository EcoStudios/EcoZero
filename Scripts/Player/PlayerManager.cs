using Player.Inventory;
using System_UI;
using UnityEngine;
using World.Items;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
    
        // Settings for player
        [Header("Player Info")]
        public float walkingSpeed;
        public float runningSpeed;
        public float jumpForce;
        public float gravity;
        
        [Header("Picking Up Info")]
        public float pickUpDistance;

        [Header("Test crap")] 
        public Item testItem;
    
    
    
        // Static Vars from above
        public static float WalkingSpeed { get; private set; }
        public static float RunningSpeed { get; private set; }
        public static float Gravity { get; private set; }
        public static float JumpForce { get; private set; }
        public static Item TestItem { get; private set; }
        public static float PickUpDistance { get; private set; }
    
        // Other Static vars

        // Object Vars
        public static GameObject PlayerGameObj { get; private set; }
        public static CharacterController PlayerController { get; private set; }
        public static Camera PlayerCamera { get; private set; }
        public static InventoryManager Inventory { get; private set; }
        public static GameObject InventoryObject { get; private set; }
        public static SelectorManager Selector { get; private set; }
        public static ActionBar ActionBar { get; private set; }
        public static GameManager GameManager { get; private set; }
        public static QuitMenu QuitMenu { get; private set; }

        void Start()
        {
            // Cursor 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
            // Vars
            PlayerGameObj = gameObject;
            PlayerController = GetComponent<CharacterController>();
            PlayerCamera = Camera.main;
            Inventory = GetComponent<InventoryManager>();
            InventoryObject = transform.Find("Inventory").gameObject;
            Selector = GameObject.Find("Selector").GetComponent<SelectorManager>();
            ActionBar = GameObject.Find("ActionBar").GetComponent<ActionBar>();
            ActionBar.Disable();
            GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            QuitMenu = GameObject.Find("QuitMenu").GetComponent<QuitMenu>();
            QuitMenu.Disable();
        
            WalkingSpeed = walkingSpeed;
            RunningSpeed = runningSpeed;
            Gravity = gravity;
            JumpForce = jumpForce;
            TestItem = testItem;
            PickUpDistance = pickUpDistance;
        }

        private void Update()
        {
            if (Input.GetKeyDown("e"))
            {
                if (InventoryObject.activeInHierarchy)
                {
                    Inventory.CloseInventory();
                }
                else
                {
                    Inventory.OpenInventory();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // To exit out of inv (another way of doin it)
                if (InventoryObject.activeInHierarchy)
                {
                    Inventory.CloseInventory();
                }
                else
                {
                    // Quit Menu
                    if (QuitMenu.gameObject.activeInHierarchy)
                    {
                        GameManager.gameState = GameManager.GameState.Playing;
                        QuitMenu.Return();
                    }
                    else
                    {
                        GameManager.gameState = GameManager.GameState.Paused;
                        QuitMenu.Enable();
                        Selector.Disable();
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }

            if (Input.GetKeyDown("1"))
            {
                Inventory.AddItem(new ItemStack(testItem, 16));
            } else if (Input.GetKeyDown("2"))
            {
                Inventory.RemoveItem(new ItemStack(testItem, 16));
            }
        }
    }
}
