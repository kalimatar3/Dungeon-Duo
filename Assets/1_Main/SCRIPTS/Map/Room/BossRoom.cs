using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : FightRoom   
{
    public string Bossname;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.Bossname = "BossPlant";
    }   
    protected override void PlaceEnemies()
    {
        if(listenemy.Count > 0) listenemy.Clear();
        Transform Boss = EnemySpawner.Instance.Spawn(Bossname,new Vector3(this.Center.x,this.Center.y),Quaternion.identity);  
        Boss.GetComponent<Enemy>().RoomHolder = this;
        listenemy.Add(Boss.GetComponent<Enemy>());
    }
    public override void CreateRoom(HashSet<Vector2Int> floorpositions)
    {
        this.Place4walls();
        base.CreateRoom(floorpositions);
    }

    private void Place4walls()
    {
        HashSet<Vector2Int> HideWall = new HashSet<Vector2Int>() {Vector2Int.down * 5,Vector2Int.left * 5,Vector2Int.right * 5, Vector2Int.up * 5};
        Wallpositions.UnionWith(HideWall);
    }
}
