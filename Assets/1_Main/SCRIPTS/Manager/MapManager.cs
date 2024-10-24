using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Video;
[DefaultExecutionOrder(-1)]
public class MapManager : LazySingleton<MapManager>
{
    public List<Room> rooms;
    public List<Corridor> corridors;
    public List<Transform> walls;
    public RoomGenerator roomGenerator;
    public RoomGenerator RoomGenerator {get {return roomGenerator;}}
    public bool IsCompletedLoadedMap;
    public int Curfloor = 0;
    public List<Room> Rooms {
        get {return rooms;}
        set {
            rooms = value;
        } 
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadroomGenerator();
    }
    protected void LoadroomGenerator() {
        this.roomGenerator = GetComponentInChildren<RoomGenerator>();
    }

    public void ClearRooms()
    {
        foreach(var ele in rooms) {
            ele.DeSpawn();
        }
        foreach(var ele in corridors) {
            ele.DeSpawn();
        }
        foreach(var ele in walls) {
            MapSpawner.Instance.DeSpawnToPool(ele);
        }
        TilemapVisualizer.instance.Clear();
        this.rooms.Clear();
        this.corridors.Clear();
        walls.Clear();
    }
    public void GenerateFirstfloor() {
        this.roomGenerator.CreateRooms(Curfloor);
        this.InitPlayer();
        StartCoroutine(this.Crloading());
        StartCoroutine(CrCheckisCompletedLoad());
    }
    protected IEnumerator Crloading() {
        PanelManager.instance.getPanelbyName("Loading_Panel").gameObject.SetActive(true);
        yield return new WaitUntil(predicate:()=> {
            if(!IsCompletedLoadedMap) return false;
            return true;
        });
        PanelManager.instance.ReturntoMainMenu();
        PanelManager.instance.getPanelbyName("GamePlay_Panel").GetComponent<GamePlay_Panel>().Noticefloor();
    }
    protected IEnumerator CrCheckisCompletedLoad() {
        this.IsCompletedLoadedMap = false;
        yield return new WaitUntil(predicate:()=> {
            if(rooms == null) return false;
            if(rooms.Count <=0) return false;
            if(Player.instance == null)return false;
            return true;
        });
    this.IsCompletedLoadedMap = true;
    }
    public void InitPlayer()  {
        if(Player.instance) {
            Player.instance.transform.position = new Vector3(Rooms[0].Center.x,rooms[0].Center.y);
        }
        else {
            Player player = DataManager.instance.GetPlayerbyName(DataManager.instance.CurCharacterData.Name);
            GameObject playerObj =  Instantiate(player).gameObject;
            playerObj.transform.position = new Vector3(Rooms[0].Center.x,rooms[0].Center.y);
            player.Level = DataManager.instance.CurCharacterData.level;
        }
    }
}
