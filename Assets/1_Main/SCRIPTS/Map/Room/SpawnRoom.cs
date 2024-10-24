using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : Room
{
    public override void CreateRoom(HashSet<Vector2Int> floorpositions)
    {
        base.CreateRoom(floorpositions);
        ItemSpawner.Instance.Spawn("FireStaff",new Vector3(Center.x + 1, Center.y + 2,0),Quaternion.identity);
        ItemSpawner.Instance.Spawn("NormalBow",new Vector3(Center.x - 1, Center.y + 2,0),Quaternion.identity);
        ItemSpawner.Instance.Spawn("NormalSword",new Vector3(Center.x + 2, Center.y + 2,0),Quaternion.identity);
    }

    protected override void TriggerState()
    {
        this.statemachine.ChangeSate(openState);
    }
}
