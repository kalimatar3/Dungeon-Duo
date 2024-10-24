using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyDealer,IDameable
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = false;
        this.CanDepspawnbyhit  = true;
        this.CanDespawnbytime  = true;
    }
    public void Dead()
    {
        EnemySchemeSpawner.Instance.DeSpawnToPool(this.transform);
    }
    public void DeductHp(float dame)
    {
    }
    public void IcrHP(float number)
    {
    }

    public void Knockback(IHitable hitable, float power)
    {
    }
    public void Stun(float time)
    {
    }

}
