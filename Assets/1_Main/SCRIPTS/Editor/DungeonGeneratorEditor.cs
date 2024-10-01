
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SimpleRandomWalkDungeonGenerator),true)]
public class DungeonGeneratorEditor : Editor
{
    DungeonGenerator dungeonGenerator;
    protected void Awake() {
        dungeonGenerator = (DungeonGenerator)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            this.dungeonGenerator.GenerateDungeon();
        }
    }
}
