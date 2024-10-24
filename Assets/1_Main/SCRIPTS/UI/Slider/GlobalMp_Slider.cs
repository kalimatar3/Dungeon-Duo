using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMp_Slider : BaseSlider
{
    protected void FixedUpdate() {
        if(Player.Instance == null) return;
        if(Player.Instance.MaxMp != 0) this.SetvalueSlider(Player.Instance.CurMp/Player.Instance.MaxMp);
    }
}
