using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class MeleeAttack : PlayerDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = true;
        this.canDeSpawnbyhit = false;
        this.canDespawnbytime = true;
    }
    public override void DoEffect(IDameable dameable)
    {
        base.DoEffect(dameable);
        dameable.Knockback(this,3);
    }
    public override void Hit(IDameable dameable)
    {
        base.Hit(dameable);
        if(dameable.GetTransform().GetComponent<EnemyRangedAttack>()) dameable.Dead();
    }
}
