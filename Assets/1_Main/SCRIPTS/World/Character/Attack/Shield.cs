using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PlayerDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.canDeSpawnbyhit = false;
        this.canDespawnbytime  = true;
        this.Multicalhit = true;
        this.Dame = 0;
    }
    public override void Hit(IDameable dameable)
    {
        base.Hit(dameable);
        if(dameable.GetTransform().GetComponent<EnemyRangedAttack>()) dameable.Dead();
    }
}
