using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackingState : EnemyState
{    
    public EnemyTrackingState(EnemyStateMachine statemachine, Enemy enemy) : base(statemachine, enemy)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Movement.Speed = enemy.SO.TrackingSpeed;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        this.enemy.Movement.Tracking();
        if(enemy.TarGet != null)
        {
            this.enemy.Movement.FacingtoTarget(enemy.TarGet.transform);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }

}
