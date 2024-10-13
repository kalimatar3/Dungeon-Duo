using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy
{
    public override void Attack()
    {
        this.ExploseSeft();
    }
    protected void ExploseSeft() {
        EnemySchemeSpawner.Instance.Spawn("Explosion",this.transform.position,Quaternion.identity);
        this.DeSpawn();   
    }
}
