using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBow : Bow
{
    public override void AttackScheme()
    {
        Quaternion quater = Quaternion.LookRotation(this.transform.forward,guntip.transform.up);
        Transform arrow = WeaponAttackSpawner.Instance.Spawn("NormalArrow",guntip.transform.position,quater);
    }
}
