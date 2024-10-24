using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalHp_Slider : BaseSlider
{
    protected void FixedUpdate() {
        if(Player.Instance == null) return;
        if(Player.Instance.MaxHp != 0) this.SetvalueSlider(Player.Instance.Reciver.Hp/Player.Instance.MaxHp);
    }
}
