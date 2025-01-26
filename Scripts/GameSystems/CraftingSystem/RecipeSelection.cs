using System;
using UnityEngine;

namespace GameSystems.CraftingSystem
{
    public class RecipeSelection : MonoBehaviour
    {
    
        private CraftingTable _craftingTable;
        private GameObject _currentHoveredObject;
        
        public event Action<RecipeSlot> OnRecipeSelected;

        void Start()
        {
            _craftingTable = gameObject.GetComponent<CraftingTable>();
        }

        void Update()
        {
            if (!_craftingTable.IsOpened) return;
            
            // Gets current hovered gameobject via raycast
            try
            {
                RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
                _currentHoveredObject = hit.collider.transform.parent.gameObject;
            }
            catch (NullReferenceException)
            {
                _currentHoveredObject = null;
            }
            

            // Invoking the recipe selected event when click on a slot
            if (Input.GetMouseButtonDown(0) 
                && _currentHoveredObject && _currentHoveredObject.GetComponent<RecipeSlot>())
            {
                OnRecipeSelected?.Invoke(_currentHoveredObject.GetComponent<RecipeSlot>());
            }
            
        }


    }
}
