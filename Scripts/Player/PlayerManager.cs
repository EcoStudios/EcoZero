using Player.Inventory;
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

        [Header("Test crap")] 
        public Item testItem;
    
    
    
        // Static Vars from above
        public static float WalkingSpeed { get; private set; }
        public static float RunningSpeed { get; private set; }
        public static LayerMask GroundLayer { get; private set; }
        public static float Gravity { get; private set; }
        public static float JumpForce { get; private set; }
        public static Item TestItem { get; private set; }
    
        // Other Static vars

        // Object Vars
        public static GameObject PlayerGameObj { get; private set; }
        public static CharacterController PlayerController { get; private set; }
        public static GameObject GroundCheck { get; private set; }
        public static Camera PlayerCamera { get; private set; }
        public static InventoryManager Inventory { get; private set; }
        public static GameObject InventoryObject { get; private set; }

        void Start()
        {
            // Cursor 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
            // Vars
            PlayerGameObj = gameObject;
            PlayerController = GetComponent<CharacterController>();
            GroundCheck = GameObject.Find("GroundCheck");
            GroundLayer = LayerMask.GetMask("Ground");
            PlayerCamera = Camera.main;
            Inventory = GetComponent<InventoryManager>();
            InventoryObject = transform.Find("Inventory").gameObject;
        
            WalkingSpeed = walkingSpeed;
            RunningSpeed = runningSpeed;
            Gravity = gravity;
            JumpForce = jumpForce;
            TestItem = testItem;
        }

        private void Update()
        {
            if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Escape))
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
