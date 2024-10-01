using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class Room : MyBehaviour {
    protected HashSet<Vector2Int> floorpositions = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> Wallpositions = new HashSet<Vector2Int>();
    public Vector2Int Center;
    [SerializeField] protected List<Roomgate> listgate;
    protected RoomStatemachine statemachine;
    protected RoomCloseState closeState;
    protected RoomOpenState openState;
    public List<Roomgate> ListGate {get {return listgate;}}
    public HashSet<Vector2Int> FloorPositions {get {return floorpositions;}}
    protected virtual void OnEnable() {
        this.statemachine = new RoomStatemachine();
        this.closeState = new RoomCloseState(statemachine,this);
        this.openState = new RoomOpenState(statemachine,this);
        statemachine.Initialize(openState);
    }
    protected virtual void TriggerState() {
    }
    public virtual void CreateRoom(HashSet<Vector2Int> floorpositions) {
        this.listgate.Clear();
        this.floorpositions = floorpositions;
        TilemapVisualizer.Instance.PaintFloorTiles(floorpositions);
        Wallpositions = WallGenerator.CreateWalls(floorpositions,TilemapVisualizer.Instance);
    }
    public void CreateGate(IEnumerable<Vector2Int> positions,string name) {
        Transform gate = MapSpawner.Instance.Spawn("RoomGate",Vector3.zero,Quaternion.identity);
        gate.transform.parent = this.transform;
        gate.transform.name = name;
        gate.GetComponent<Roomgate>().Open();
        listgate.Add(gate.GetComponent<Roomgate>());
        gate.GetComponent<Roomgate>().Initinite((HashSet<Vector2Int>)positions);
    }
    protected virtual void FixedUpdate() {
        this.TriggerState();
    }
} 