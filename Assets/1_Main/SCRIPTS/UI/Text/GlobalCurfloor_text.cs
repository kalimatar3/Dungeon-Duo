using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCurfloor_text : BaseTextUI
{
    public override void ShowText()
    {
        this.text.text = "Floor " + MapManager.Instance.Curfloor.ToString("F0");
    }
}
