using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDp_text : BaseTextUI
{
    public override void ShowText()
    {
        if(Player.Instance == null) 
        {
            Debug.Log("player null");
            return;
        }
        this.text.text = Player.Instance.Reciver.Dp.ToString("F0") + "/" + Player.Instance.MaxDp.ToString("F0"); 
    }
}
