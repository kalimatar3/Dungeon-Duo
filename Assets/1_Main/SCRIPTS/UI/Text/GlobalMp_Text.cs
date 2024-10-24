using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMp_Text : BaseTextUI
{
    public override void ShowText()
    {
        if(Player.Instance) this.text.text = Player.Instance.CurMp.ToString("F0") + "/" + Player.Instance.MaxMp.ToString("F0");
    }
}
