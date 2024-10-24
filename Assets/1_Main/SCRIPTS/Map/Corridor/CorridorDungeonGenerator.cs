using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CorridorDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5,corridorWidth = 3;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }
    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> rooms = CreateRooms(potentialRoomPositions);

        HashSet<Vector2Int> deadEnds = FindAllDeadEnd(floorPositions); 
        HashSet<Vector2Int> roominDeadEnd = CreateRoominDeadEnds(deadEnds);
        rooms.UnionWith(roominDeadEnd);
        

       floorPositions.UnionWith(rooms);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        //WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(dungeonGenaratorparameter, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }
    private HashSet<Vector2Int> FindAllDeadEnd(HashSet<Vector2Int> floorPositions) {
        HashSet<Vector2Int> DeadEnds = new HashSet<Vector2Int>();
        foreach(Vector2Int position in floorPositions)
        {
            int neighbourCounts = 0;
            foreach(var direction in Direction2D.cardinalDirectionsList) {
                if(floorPositions.Contains(position + direction * (corridorWidth/2 +1))) neighbourCounts ++;
                }
            if(neighbourCounts <= 1) {
                DeadEnds.Add(position);
            }
        }
        return DeadEnds;
    }
    private HashSet<Vector2Int> CreateRoominDeadEnds(HashSet<Vector2Int> DeadEnds) {
        HashSet<Vector2Int> roompositions = new HashSet<Vector2Int>();
        foreach(Vector2Int position in DeadEnds) {
            var roomfloor = RunRandomWalk(dungeonGenaratorparameter,position);
            roompositions.UnionWith(roomfloor);            
        }
        return roompositions;
    }
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = StartPosition;
        potentialRoomPositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength,corridorWidth);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}