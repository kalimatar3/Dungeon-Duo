using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_button : BaseButton
{
    public PlayerSkillCoolDown_slider playerSkillCoolDown_Slider;
    public override void OnClick()
    {
        Player.Instance.DoSkill();
    }
}
