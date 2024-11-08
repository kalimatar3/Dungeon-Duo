using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGlobal_Text : BaseTextUI
{
    public override void ShowText()
    {
        text.text = MapManager.Instance.Goldnumber.ToString("F0");
    }
}
