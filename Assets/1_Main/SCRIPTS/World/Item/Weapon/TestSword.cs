using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSword : Sword
{
    public override void Attack()
    {
        base.Attack();
        Quaternion quaternion = Quaternion.LookRotation(this.transform.forward,this.transform.up);
        Transform attack =  WeaponAttackSpawner.Instance.Spawn("MeleeAttack",this.transform.position,quaternion);
        attack.GetComponent<CharacterDealer>().Dame = this.dame;
    }
    protected override void InitData()
    {
    }
    protected override void InitStatistics()
    {
        dame = 1;
        firerate = 0.5f;
        range = 1;
    }
}
