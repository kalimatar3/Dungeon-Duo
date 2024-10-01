using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightRoom : Room
{
    [SerializeField] protected List<Enemy> listenemy;
    public List<Enemy> ListEnemy {get {return listenemy;}}
    public override void CreateRoom(HashSet<Vector2Int> floorpositions)
    {
        base.CreateRoom(floorpositions);
        this.PlaceEnemies();
    }
    protected void PlaceEnemies() {
        if(listenemy.Count > 0) listenemy.Clear();
        HashSet<Vector2Int> EnemyPos = this.InitListEnemyPos();
        foreach(var ele in EnemyPos) {
            Transform enemy = EnemySpawner.Instance.Spawn("TestEnemy",new Vector3(ele.x,ele.y,0),Quaternion.identity);
            listenemy.Add(enemy.GetComponent<Enemy>());
        }
    }
    protected HashSet<Vector2Int> InitListEnemyPos() {
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        List<Vector2Int> listfloorpos = new List<Vector2Int>(floorpositions);
        int NumberofEnemies = 1;
        int i = 0;
        while(i < NumberofEnemies) 
        {
            Vector2Int position = listfloorpos[UnityEngine.Random.Range(0,listfloorpos.Count)];
            HashSet<Vector2Int> neighborpositions = new HashSet<Vector2Int>();
            foreach(var neighbor in Direction2D.eightDirectionsList) {
                neighborpositions.Add(position + neighbor);
            }
            if(neighborpositions.IsProperSubsetOf(floorpositions)) {
                positions.Add(position);
                i++;
            }
        }
        return positions;
    }
    protected override void TriggerState()
    {
        base.TriggerState();
        foreach (var ele in listenemy)
        {
            if(!ele.Reciver.IsDead) {
                if(ele.StateMachine.CurState.GetType() == typeof(EnemyTrackingState)||ele.StateMachine.CurState.GetType() == typeof(EnemyAttackState))
                {
                    statemachine.ChangeSate(closeState);
                    return;
                }
            }
        }
        this.statemachine.ChangeSate(openState);
    }
}
