using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected EnemyStateMachine stateMachine;
    public EnemyState(EnemyStateMachine statemachine,Enemy enemy) : base(statemachine)
    {
        this.enemy = enemy;
        this.stateMachine = statemachine;
    }
}
