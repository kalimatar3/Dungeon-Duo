using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class RoomCloseState : RoomState
{
    public RoomCloseState(RoomStatemachine statemachine, Room room) : base(statemachine, room)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        this.room.CurstateName = "CloseState";
        foreach(var ele in room.ListGate) {
            ele.Close();
        }
        if(room.GetComponent<FightRoom>())
        {
            foreach(var ele in room.GetComponent<FightRoom>().ListEnemy) {
                ele.CanDetected  = true;
            }
        }
    }
}
