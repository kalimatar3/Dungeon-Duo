using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MeleeEnemy
{
    public override void Attack()
    {
        if(target == null) return;
        Vector3 dir = (target.transform.position - this.transform.position).normalized;
        Quaternion quater = Quaternion.LookRotation(this.transform.forward,dir);
        Transform meleeattack = EnemySchemeSpawner.Instance.Spawn("MeleeAttack",this.transform.position + dir,quater);
        meleeattack.GetComponent<EnemyDealer>().Dame = 1;
    }
}
