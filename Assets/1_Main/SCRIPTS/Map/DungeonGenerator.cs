using UnityEngine;
public abstract class DungeonGenerator : MonoBehaviour 
{
    [SerializeField] protected Vector2Int StartPosition = Vector2Int.zero;
    [SerializeField] protected TilemapVisualizer tilemapVisualizer;
    [SerializeField] protected DungeonGenaratorSO dungeonGenaratorparameter;
    protected abstract void RunProceduralGeneration(); 
    public void GenerateDungeon() {
        this.tilemapVisualizer.Clear();
        this.RunProceduralGeneration();
    }  
}