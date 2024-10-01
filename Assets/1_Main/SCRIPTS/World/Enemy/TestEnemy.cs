using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public override void Attack()
    {
        Vector3 dir = (target.transform.position - this.transform.position).normalized;
        Quaternion quater = Quaternion.LookRotation(this.transform.forward,dir);
        Transform meleeattack = EnemySchemeSpawner.Instance.Spawn("MeleeAttack",this.transform.position + dir,quater);
        meleeattack.GetComponent<EnemyDealer>().Dame = 1;
    }
    protected override void LoadStat()
    {
        maxHP = 10;
        attackRange = 1;
        detectionRange = 5f;
        this.firerate = 1f;
    }
}
