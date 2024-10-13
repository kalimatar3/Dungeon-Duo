using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] 
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField] protected int corridorWidth = 2;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    [SerializeField]
    private bool RandomwalkRoom = false;
    protected override void RunProceduralGeneration()
    {
        this.CreateRooms();
    }
    private void OnEnable() {
        this.CreateRooms();    
    }
    protected void CreateRooms() {
        var rooms = ProceduralGenerationAlgorithms.BinarySpacePartitioning
        (new BoundsInt((Vector3Int)StartPosition,new Vector3Int(dungeonWidth,dungeonHeight,0)),minRoomWidth,minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();          
        if (RandomwalkRoom)
        {
            floor = CreateRoomsRandomly(rooms);
        }
        else
        { 
            floor = CreateSimpleRooms(rooms);
        }
        HashSet<Vector2Int> corridors = ConnectRooms(MapManager.Instance.Map);
        floor.UnionWith(corridors);
        HashSet<Vector2Int> wallpositions = WallGenerator.CreateWalls(floor,TilemapVisualizer.Instance);
        foreach(var ele in wallpositions) {
            Transform box =  MapSpawner.Instance.Spawn("WallBox",new Vector3(ele.x,ele.y,0),Quaternion.identity);
            box.transform.parent = this.transform;
        }
    }
    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            HashSet<Vector2Int> room = new HashSet<Vector2Int>();
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(dungeonGenaratorparameter, roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    room.Add(position);
                }
            }
            floor.UnionWith(room);
            if(i==0)
            {
                Transform firstroom = MapSpawner.Instance.Spawn("SpawnRoom",roomsList[i].center,Quaternion.identity);    
                firstroom.name = "FirstRoom";
                firstroom.GetComponent<Room>().Center = new Vector2Int((int)roomsList[i].center.x,(int)roomsList[i].center.y);
                firstroom.GetComponent<Room>().CreateRoom(room);
                MapManager.Instance.Map.Add(firstroom.GetComponent<Room>());     
            }
            else {
                Transform newroom = MapSpawner.Instance.Spawn("FightRoom",roomsList[i].center,Quaternion.identity);
                newroom.name = "Room " + i;
                newroom.GetComponent<Room>().Center = new Vector2Int((int)roomsList[i].center.x,(int)roomsList[i].center.y);
                newroom.GetComponent<Room>().CreateRoom(room);
                MapManager.Instance.Map.Add(newroom.GetComponent<Room>());
            }
        }
        return floor;
    }
    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Room> listroom) {
        List<Room> listroomtem = new List<Room>();
        foreach(var ele in listroom) {
            listroomtem.Add(ele);
        }
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        Room baseroom = listroomtem[UnityEngine.Random.Range(0,listroomtem.Count)];
        while(listroomtem.Count > 0) {
            Room closestrooms = Findclosestpositions(baseroom,listroomtem);
            listroomtem.Remove(closestrooms);
            Transform corridor = MapSpawner.Instance.Spawn("Corridor",new Vector3(baseroom.Center.x,baseroom.Center.y,0),Quaternion.identity);
            corridor.GetComponent<Corridor>().Initinite(baseroom,closestrooms,2);
            corridors.UnionWith(corridor.GetComponent<Corridor>().corridorPos);    
            baseroom = closestrooms;
        }
        return corridors;
    }
    private Room Findclosestpositions(Room baseroom, List<Room> listroom)
    {
            Room closestroom = baseroom;
            float min = dungeonWidth;
            foreach(var ele in listroom) {
                if(ele == baseroom) continue;
                Vector2Int dir = ele.Center - baseroom.Center;
                if(dir.magnitude < min) {
                    min = dir.magnitude;
                    closestroom = ele;
                }
            }
        return closestroom;
    }
}
