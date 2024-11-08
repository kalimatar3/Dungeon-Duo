using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MyBehaviour, Iinteractable
{
    public Transform GetTransform()
    {
        return this.transform;
    }
    public void OnInteract(Player player)
    {
        MapManager.Instance.Curfloor ++;
        MapManager.Instance.Generatefloor();
    }
}
