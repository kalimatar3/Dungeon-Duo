using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSword : Sword
{
    public override void AttackScheme()
    {
        base.AttackScheme();
        Quaternion quaternion = Quaternion.LookRotation(this.transform.forward,this.transform.up);
        Transform attack =  WeaponAttackSpawner.Instance.Spawn("MeleeAttack",this.transform.position,quaternion);
        attack.GetComponent<PlayerDealer>().Dame = this.dame;
    }
}
