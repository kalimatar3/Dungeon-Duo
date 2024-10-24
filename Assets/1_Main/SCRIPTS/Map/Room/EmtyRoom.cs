using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmtyRoom : Room
{
    protected override void TriggerState()
    {
        this.statemachine.ChangeSate(openState);
    }
}
