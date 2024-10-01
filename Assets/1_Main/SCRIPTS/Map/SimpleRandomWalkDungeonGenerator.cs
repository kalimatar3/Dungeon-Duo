using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : DungeonGenerator
{
    protected override void RunProceduralGeneration() {
        HashSet<Vector2Int> floorPostions = RunRandomWalk(this.dungeonGenaratorparameter,StartPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPostions);
       // WallGenerator.CreateWalls(floorPostions,tilemapVisualizer);
    }
    protected HashSet<Vector2Int> RunRandomWalk(DungeonGenaratorSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walklength); 
            floorPositions.UnionWith(path);
            if (parameters.StartRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
