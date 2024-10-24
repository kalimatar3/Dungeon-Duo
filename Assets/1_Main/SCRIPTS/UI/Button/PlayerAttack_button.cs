using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_button : BaseButton
{
    public override void OnClick()
    {
        Player.Instance.Attack();
    }
}
