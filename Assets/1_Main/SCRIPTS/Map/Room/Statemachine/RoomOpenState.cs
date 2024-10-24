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
        this.room.CurstateName = "OpenState";
        room.StartCoroutine(this.CrDelayopenGate());
        if(room.GetComponent<FightRoom>())
        {
            foreach(var ele in room.GetComponent<FightRoom>().ListEnemy) {
                ele.CanDetected  = false;
            }
        }

    }
    protected IEnumerator CrDelayopenGate() {
        yield return new WaitUntil(predicate:()=> {
            if(room.ListGate.Count <= 0) return false;
            return true;
        });
        foreach(var ele in room.ListGate) {
            ele.Open();
        }      
    }
}
