using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GameManager : LazySingleton<GameManager>
{
    private void Start() {
        MapManager.instance.GenerateFirstfloor();
    }
}
