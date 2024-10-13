using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : RangedEnemy
{
    public override void Attack()
    {
        if(target == null) return;
        Vector3 Dir = (target.transform.position - this.transform.position).normalized;
        Quaternion quaternion = Quaternion.LookRotation(this.transform.forward,Dir);
        EnemySchemeSpawner.Instance.Spawn("RedOrb",transform.position,quaternion);
    }
}
