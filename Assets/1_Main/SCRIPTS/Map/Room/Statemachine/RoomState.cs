using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomState : State
{
    protected Room room;
    public RoomState(RoomStatemachine statemachine,Room room) : base(statemachine)
    {
        this.room = room;
    }
}
