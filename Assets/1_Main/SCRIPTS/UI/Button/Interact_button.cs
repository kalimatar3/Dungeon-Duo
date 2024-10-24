using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_button : BaseButton
{
    public Player player;
    public override void OnClick()
    {
        Player.Instance.Interact();
    }
}
