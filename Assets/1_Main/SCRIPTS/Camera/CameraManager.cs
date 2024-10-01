using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MyBehaviour
{
    protected static CameraManager instance;
    public static CameraManager Instace {get => instance;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }
    
}
