using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace System_UI
{
    public class ActionBar : MonoBehaviour
    {

        private TMP_Text _actionBarText;

        private int _timerSeconds;

        public ActionBarPriority currentPriority = ActionBarPriority.Lowest;

        public enum ActionBarPriority
        {
            Highest = 2,
            Medium = 1,
            Lowest = 0
        }

        void Start()
        {
            _actionBarText = GetComponentInChildren<TMP_Text>();
        }
    
        public void Disable()
        {
            SetText("", ActionBarPriority.Highest);
            currentPriority = ActionBarPriority.Lowest;
            _timerSeconds = 0;
            gameObject.SetActive(false);
        }

        
        public async Task Enable(bool hasTimer = false)
        {
            gameObject.SetActive(true);
            if (hasTimer)
            {
                await Task.Delay(TimeSpan.FromSeconds(_timerSeconds));
                Disable();
            }
        }

        public void SetText(string text, ActionBarPriority priority)
        {
            if (currentPriority > priority) return;
            currentPriority = priority;
            _actionBarText.text = text;
        }
        public void SetTextColor(Color color) { _actionBarText.color = color; }
    
        public void SetTimer(int seconds) { _timerSeconds = seconds; }
        


    }
}
