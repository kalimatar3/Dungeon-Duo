using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDungeonRoom : Room
{
    protected Transform Portal;
    protected void SpawnPortal() {
        //Spawn A Protal to next Dungeon at center of the room;
        Portal =  MapSpawner.Instance.Spawn("Portal",new Vector3(this.Center.x,Center.y),Quaternion.identity);
    }
    public override void CreateRoom(HashSet<Vector2Int> floorpositions)
    {
        base.CreateRoom(floorpositions);
        this.SpawnPortal();
    }
    public override void DeSpawn()
    {
        base.DeSpawn();
        MapSpawner.Instance.DeSpawnToPool(Portal);
    }

    protected override void TriggerState()
    {
        this.statemachine.ChangeSate(openState);
    }
}
