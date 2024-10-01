using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderingState : EnemyState
{
    public EnemyWanderingState(EnemyStateMachine statemachine, Enemy enemy) : base(statemachine, enemy)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void FrameUpdate()
    {
        EnemyMovement movement = enemy.Movement;
        base.FrameUpdate();
        movement.Wandering();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
