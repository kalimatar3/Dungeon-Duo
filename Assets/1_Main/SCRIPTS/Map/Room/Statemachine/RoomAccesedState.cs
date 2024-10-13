using UnityEngine;

public class RoomAccesedState : RoomState
{
    protected FightRoom fightRoom;

    public RoomAccesedState(RoomStatemachine statemachine, Room room) : base(statemachine, room)
    {
        this.fightRoom = (FightRoom)room;
    }
    public override void EnterState()
    {
        base.EnterState();
        foreach(var ele in fightRoom.ListEnemy) {
            ele.CanDetected = true;
        }
    }
}