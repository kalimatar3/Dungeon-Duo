using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Statemachine Statemachine {  get; private set; }
    public State (Statemachine statemachine)
    {
        this.Statemachine = statemachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate () { }
}
