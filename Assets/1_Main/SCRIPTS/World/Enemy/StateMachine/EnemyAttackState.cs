using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float timer;
    public EnemyAttackState(EnemyStateMachine statemachine, Enemy enemy) : base(statemachine, enemy)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = 0;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer += Time.deltaTime *1f;
        if(timer < enemy.Firerate) 
        {
            stateMachine.CanChange = false;
            return;
        }
        this.enemy.Attack();
        this.stateMachine.CanChange = true;
        this.timer = 0;      
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
