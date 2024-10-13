using UnityEngine;
public class EnemyWanderingState : EnemyState
{
    public EnemyWanderingState(EnemyStateMachine statemachine, Enemy enemy) : base(statemachine, enemy)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Movement.Speed = enemy.SO.Wandaspeed; 
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.Movement.Wandering();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
