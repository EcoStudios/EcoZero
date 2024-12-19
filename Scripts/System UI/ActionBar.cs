using System;
using TMPro;
using UnityEngine;

public class ActionBar : MonoBehaviour
{

    public TMP_Text ActionBarText { get; private set; }

    void Start()
    {
        ActionBarText = GetComponentInChildren<TMP_Text>();
    }
    
    public void Disable() { gameObject.SetActive(false); }

    public void Enable() { gameObject.SetActive(true); }
    
    public void SetText(string text) { ActionBarText.text = text; }

    public void Reset()
    {
        ActionBarText.text = "NULL";
        Disable();
    }
}
