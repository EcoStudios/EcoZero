using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace System_UI
{
    public class SelectorManager : MonoBehaviour
    {
        
        public event Action<GameObject> OnHoverOnObject;
        
        public Image SelectorImage { get; private set; }
        

        public void Enable() { gameObject.SetActive(true); }
        public void Disable() { gameObject.SetActive(false); }
        public bool IsEnabled() { return gameObject.activeSelf; }

        void Start()
        {
            SelectorImage = GetComponentInChildren<Image>(true);
        }

        // Gets a raycast to see if there's an item hovered over by the selector, if so the event will be invoked.
        void Update()
        {
            
            PlayerManager.PlayerCamera.transform.GetPositionAndRotation(out var pos, out var rot);
            Vector3 origin = pos + rot * Vector3.forward;
            
            try
            {
                Physics.Raycast(origin, PlayerManager.PlayerCamera.transform.forward,
                    out RaycastHit hit, PlayerManager.PickUpDistance);
                if (!hit.transform.gameObject) return; // Returns if null
                OnHoverOnObject?.Invoke(hit.rigidbody.gameObject);
            }
            catch (NullReferenceException) { }
        }
        
        
        
    }
}
