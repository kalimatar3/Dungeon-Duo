using System;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MyBehaviour, IDespawnable 
{
    [SerializeField] public HashSet<Vector2Int> corridorPos;
    public void Initinite(Room baseroom,Room targetroomm,int width) {
        corridorPos = new HashSet<Vector2Int>();
        corridorPos = this.CreateCorridor(baseroom,targetroomm,width);  
        TilemapVisualizer.Instance.PaintFloorTiles(corridorPos);
    }
    private HashSet<Vector2Int> CreateCorridor(Room baseroom, Room destinationroom,int width)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        HashSet<Vector2Int> basegateroompositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> targetgaterompostions = new HashSet<Vector2Int>();
        Stack<GateDetected> basestack = new Stack<GateDetected>();
        Queue<GateDetected> targetqueue = new Queue<GateDetected>();
        Vector2Int dir = Vector2Int.up;
        var position = baseroom.Center;
        corridor.Add(position);
        while (position.y != destinationroom.Center.y)
        {
            if(destinationroom.Center.y > position.y)
            {
                position += Vector2Int.up;
                dir = Vector2Int.right;
            }
            else if(destinationroom.Center.y < position.y)
            {
                position += Vector2Int.down;
                dir = Vector2Int.left;
            
            }
            for(int j = -width/2 ; j < width/2 + width % 2 ; j++) {
                Vector2Int newpos = position + dir * j; 
                corridor.Add(newpos);
                Debug.Log(newpos);
                foreach(var ele in baseroom.Wallpositions) {
                    if((int)ele.x == newpos.x && (int)ele.y == newpos.y) {
                        GateDetected gateDetected = new GateDetected() {
                            position = ele,
                            index = j + width/2,
                            direction = dir
                        };
                        basestack.Push(gateDetected);
                    }
                }
                foreach(var ele in destinationroom.Wallpositions) {
                    if((int)ele.x == newpos.x && (int)ele.y == newpos.y) {
                        GateDetected gateDetected = new GateDetected() {
                            position = ele,
                            index = j + width/2,
                            direction = dir
                        };
                        targetqueue.Enqueue(gateDetected);
                    }
                }
            }
        }
        while (position.x != destinationroom.Center.x)
        {
            if (destinationroom.Center.x > position.x)
            {
                position += Vector2Int.right;
                dir = Vector2Int.down;
            }
            else if(destinationroom.Center.x < position.x) 
            {
                position += Vector2Int.left;
                dir = Vector2Int.up;
            }
            for(int j = -width/2 ; j < width/2 + width % 2 ; j++) {
                Vector2Int newpos = position + dir * j; 
                corridor.Add(newpos);
                Debug.Log(newpos);
                foreach(var ele in baseroom.Wallpositions) {
                    if((int)ele.x == newpos.x && (int)ele.y == newpos.y) {
                        GateDetected gateDetected = new GateDetected() {
                            position = ele,
                            index = j + width/2,
                            direction = dir
                        };
                        basestack.Push(gateDetected);
                    }
                }
                foreach(var ele in destinationroom.Wallpositions) {
                    if((int)ele.x == newpos.x && (int)ele.y == newpos.y) {
                        GateDetected gateDetected = new GateDetected() {
                            position = ele,
                            index = j + width/2,
                            direction = dir
                        };
                        targetqueue.Enqueue(gateDetected);
                    }
                }
            }
        }
        if(basestack.Count > 0) basegateroompositions = CreateListGatePos(basestack.Pop(),width);
        if(targetqueue.Count > 0) targetgaterompostions = CreateListGatePos(targetqueue.Dequeue(),width);
        baseroom.CreateGate(basegateroompositions,"baseGate");
        destinationroom.CreateGate(targetgaterompostions,"targetGate");
        return corridor;
    }
    protected HashSet<Vector2Int> CreateListGatePos(GateDetected gateDetected,int width) {
        int i = gateDetected.index;
        Vector2Int pos = gateDetected.position;
        HashSet<Vector2Int> lispos = new HashSet<Vector2Int>();
        do {
            lispos.Add(pos);
            i = (i + 1) % (width);
            pos += (i - gateDetected.index)* gateDetected.direction;
        }
        while (i != gateDetected.index);
        return lispos;
    }

    public void DeSpawn()
    {
        MapSpawner.Instance.DeSpawnToPool(this.transform);
    }
}
public struct GateDetected {
    public Vector2Int position;
    public int index;
    public Vector2Int direction;
}
