using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class MapManager : LazySingleton<MapManager>
{
    [SerializeField] protected List<Room> map;
    public List<Room> Map {
        get {return map;}
        set {
            map = value;
        } 
    }
}
