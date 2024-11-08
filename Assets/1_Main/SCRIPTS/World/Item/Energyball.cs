using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Energyball : Property
{
    public override void OnInteract(Player player)
    {
        if(!Dropdone) return;
        DataManager.Instance.propertyDynamicData.Energy += Value;
        this.DeSpawn();
    }
}