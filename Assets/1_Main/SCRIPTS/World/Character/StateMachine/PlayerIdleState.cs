using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, Statemachine statemachine) : base(player, statemachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        character.Movement.IsFacing = false;
        character.Canattack = true;
        character.Caninteract = true;
        character.minnoticebox = character.NoticeBox;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        this.character.CrossHair.InfrontofPlayer();
    }
}
