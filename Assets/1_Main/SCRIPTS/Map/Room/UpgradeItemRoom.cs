using System.Collections.Generic;
using UnityEngine;
public class UpgradeItemRoom : Room
{
    protected override void TriggerState()
    {
        this.statemachine.ChangeSate(openState);
    }
    public override void CreateRoom(HashSet<Vector2Int> floorpositions)
    {
        base.CreateRoom(floorpositions);
        this.PlaceUpgradeShop();
    }
    protected virtual void PlaceUpgradeShop() {
       MapSpawner.Instance.Spawn("UpgradeShop",new Vector3(Center.x,Center.y),Quaternion.identity);
    }
}
