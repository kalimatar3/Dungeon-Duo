using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, Statemachine statemachine) : base(player, statemachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter IdleState");
        character.Movement.IsFacing = false;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        this.character.CrossHair.InfrontofPlayer();
    }
}
