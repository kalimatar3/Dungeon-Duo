using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon_button : BaseButton
{
    public Image Icon;
    public Message message;
    public override void OnClick()
    {
        if(Player.Instance == null) return;
        Player.Instance.Equipment.ChangeWeapon();
    }
}
