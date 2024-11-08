using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Gold : Property
{
    public override void OnInteract(Player player)
    {
        if(!Dropdone) return;
        MapManager.Instance.Goldnumber += Value;
        this.DeSpawn();
    }
}