using UnityEngine;
using UnityEngine.UI;

namespace System_UI
{
    public class SelectorManager : MonoBehaviour
    {

        public Image SelectorImage { get; private set; }

        public void Enable() { gameObject.SetActive(true); }
        public void Disable() { gameObject.SetActive(false); }
        public bool IsEnabled() { return gameObject.activeSelf; }

        void Start()
        {
            SelectorImage = GetComponentInChildren<Image>(true);
        }
    }
}
