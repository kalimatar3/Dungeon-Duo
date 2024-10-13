using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bow : Weapon
{
    [SerializeField] protected Transform guntip;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadguntip();
    }
    protected void Loadguntip() {
        this.guntip = this.transform.Find("Guntip");
    }
}
