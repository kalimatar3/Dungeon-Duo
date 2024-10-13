using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : EnemyDealer
{
    [SerializeField] protected float force;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = true;
        this.CanDespawnbytime = true;
        this.CanDepspawnbyhit = false;
    }
    protected override void DealDame(float dame, IDameable dameable)
    {
        base.DealDame(dame, dameable);
        this.DoEffect(dameable);
    }
    public override void DoEffect(IDameable dameable)
    {
        base.DoEffect(dameable);
        dameable.Knockback(this,force);
        dameable.Stun(1f);
    }
}
