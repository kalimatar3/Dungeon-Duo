using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using DG.Tweening;
using UnityEngine;

public class EnemyWalledState : EnemyState
{
    [SerializeField] protected List<Vector2Int> shortestpath;
    [SerializeField] protected int curindex = 0;
    public EnemyWalledState(EnemyStateMachine statemachine, Enemy enemy) : base(statemachine, enemy)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Movement.Speed = enemy.SO.TrackingSpeed;
        Debug.Log(enemy.transform.name + " Enter WalledState");
        curindex = 0;
        shortestpath =  new List<Vector2Int>();
        Vector2Int Targetposition = new Vector2Int((int)enemy.TarGet.transform.position.x,(int)enemy.TarGet.transform.position.y);
        shortestpath = AstarAlgorthm.FindPath(new Vector2Int((int)enemy.transform.position.x,(int)enemy.transform.position.y),Targetposition,enemy.RoomHolder.FloorPositions);
        if(shortestpath == null)
        {
            foreach(var ele in Direction2D.eightDirectionsList) {
                shortestpath = AstarAlgorthm.FindPath(new Vector2Int((int)enemy.transform.position.x,(int)enemy.transform.position.y),Targetposition + ele,enemy.RoomHolder.FloorPositions);
                if(shortestpath != null) break;
            }
        }
        if(shortestpath == null) 
        {
            enemy.StateMachine.ChangeSate(enemy.WanderingState);
            return;
        }
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(curindex >= shortestpath.Count) 
        {
            Debug.Log("Done");
            EnterState();
        }
        enemy.Movement.MovetoPosition(new Vector3(shortestpath[curindex].x + 0.5f,shortestpath[curindex].y + 0.5f));
        float offset = enemy.Movement.Speed * Time.deltaTime *2f;
        if(enemy.transform.position.x > shortestpath[curindex].x + 0.5f - offset && enemy.transform.position.x  < shortestpath[curindex].x + 0.5f + offset
        && enemy.transform.position.y > shortestpath[curindex].y + 0.5f - offset && enemy.transform.position.y < shortestpath[curindex].y + 0.5f + offset)
        {
            if(curindex < shortestpath.Count) curindex ++;   
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
