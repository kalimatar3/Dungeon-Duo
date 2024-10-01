using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DungeonGenratorSO", menuName = "ScriptableObjects/DungeonGenratorSO")]
public class DungeonGenaratorSO : ScriptableObject
{
    public int iterations = 10;
    public int walklength = 10;
    public bool StartRandomlyEachIteration = true;

}
