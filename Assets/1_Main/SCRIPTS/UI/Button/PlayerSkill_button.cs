using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_button : BaseButton
{
    public override void OnClick()
    {
        Player.Instance.DoSkill();
    }
}
