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
    public override void UpgradeFormula(int level)
    {
        this.dame = SO.Dame * (1+ level * 0.12f);
        this.firerate = Mathf.Max(SO.Firerate/2,SO.Firerate * (1 - level * 0.2f));
    }
    public override void OnEquip(Player player)
    {
        base.OnEquip(player);
        this.model.transform.localRotation = Quaternion.Euler(0,0,90);
    }
}
