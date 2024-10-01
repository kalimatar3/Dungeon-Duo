using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpenState : RoomState
{
    public RoomOpenState(RoomStatemachine statemachine, Room room) : base(statemachine, room)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        foreach(var ele in room.ListGate) {
            ele.CanOpen = true;
        }
    }
}
