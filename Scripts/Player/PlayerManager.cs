using Player.Inventory;
using Player.Systems;
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
        public static CraftingSystem CraftingSystem { get; private set; }
        

        void Start()
        {
            
            
            // Vars
            WalkingSpeed = walkingSpeed;
            RunningSpeed = runningSpeed;
            Gravity = gravity;
            JumpForce = jumpForce;
            TestItem = testItem;
            PickUpDistance = pickUpDistance;
            
            PlayerGameObj = gameObject;
            PlayerController = GetComponent<CharacterController>();
            Inventory = GetComponent<InventoryManager>();
            PlayerCamera = Camera.main;
            InventoryObject = transform.Find("Inventory").gameObject;
            Selector = GameObject.Find("Selector").GetComponent<SelectorManager>();
            ActionBar = GameObject.Find("ActionBar").GetComponent<ActionBar>();
            ActionBar.Disable();
            CraftingSystem = new CraftingSystem();
        }
        
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (InventoryObject.activeInHierarchy)
                {
                    GameManager.activeGameState = GameManager.GameState.Playing;
                    Inventory.CloseInventory();
                }
                else
                {
                    if (GameManager.activeGameState != GameManager.GameState.Playing) return;
                    GameManager.activeGameState = GameManager.GameState.ActiveInGameUI;
                    Inventory.OpenInventory();
                }
            } else if (Input.GetKeyDown(KeyCode.Escape))
            {
                // To exit out of inv (another way of doin it)
                if (InventoryObject.activeInHierarchy)
                {
                    Inventory.CloseInventory();
                    GameManager.activeGameState = GameManager.GameState.Playing;
                }
                else
                {
                    if (GameManager.activeGameState == GameManager.GameState.Playing)
                    {
                        // Exit Menu
                        GameManager.activeGameState = GameManager.GameState.ActiveInGameUI;
                        GameManager.EnableCursor();
                        _ = new ExitMenu();
                    }
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Inventory.AddItem(new ItemStack(testItem, 16));
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Inventory.RemoveItem(new ItemStack(testItem, 16));
            }
        }
    }
}
