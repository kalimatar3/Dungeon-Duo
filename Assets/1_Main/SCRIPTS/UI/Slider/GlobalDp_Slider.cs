using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDp_Slider : BaseSlider
{
    private void FixedUpdate() {
        if(Player.Instance == null) return;
        if(Player.Instance.MaxDp != 0) this.SetvalueSlider(Player.Instance.Reciver.Dp/Player.Instance.MaxDp);    
    }
}
