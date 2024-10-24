using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseTextUI : MyBehaviour
{
     [SerializeField] protected TextMeshProUGUI text;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadText();
    }
    protected void LoadText() {
        this.text = GetComponent<TextMeshProUGUI>();
        if(text == null) Debug.LogWarning("Cant found Text");
    }
    protected virtual void OnEnable() {
        this.ShowText();
    }
    public static string FormatNumberWithDots(int number)
    {
        string strNumber = number.ToString();        
        string formattedStr = "";
        for (int i = strNumber.Length - 1; i >= 0; i--)
        {
            formattedStr = strNumber[i] + formattedStr;
            if ((strNumber.Length - i) % 3 == 0 && i != 0)
            {
                formattedStr = "." + formattedStr;
            }
        }
        
        return formattedStr;
    }
    public abstract void ShowText();
    protected void FixedUpdate()
    {
        this.ShowText();
    }
}
