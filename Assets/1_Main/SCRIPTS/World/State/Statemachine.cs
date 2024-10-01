using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine
{
    public State CurState;
    public bool CanChange = true;
    public virtual void Initialize(State state)
    {
        this.CurState = state;
        this.CurState.EnterState();
    }
    public virtual void ChangeSate(State nextState)
    {
        if(!CanChange) return;
        if (nextState == CurState) return;
        CurState.ExitState();
        CurState = nextState;
        CurState.EnterState();
    }
}
