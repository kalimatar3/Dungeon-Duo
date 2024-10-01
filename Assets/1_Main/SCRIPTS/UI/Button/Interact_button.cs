using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_button : BaseButton
{
    public Player player;
    public override void Act()
    {
        this.player.Interact();
    }
}
