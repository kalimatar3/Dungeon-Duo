using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
 public delegate void StateEvent();
    public StateEvent OnEnter,OnExit,OnUpdate;
    public Statemachine Statemachine {  get; private set; }
    public State (Statemachine statemachine)
    {
        this.Statemachine = statemachine;
    }
    public virtual void EnterState() {
        if(OnEnter != null) this.OnEnter();
     }
    public virtual void ExitState() {
        if(OnExit != null) this.OnExit();
     }
    public virtual void FrameUpdate () {
        if(OnUpdate != null) this.OnUpdate();
     }}
