using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : PlayerState
{
    public PlayerDetectedState(Player player, Statemachine statemachine) : base(player, statemachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter DetectedState");
        this.character.Movement.IsFacing = true;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        this.character.Movement.FacingtoTarget(character.Target);
        this.character.CrossHair.FollowTarget(character.Target);
    }
}
