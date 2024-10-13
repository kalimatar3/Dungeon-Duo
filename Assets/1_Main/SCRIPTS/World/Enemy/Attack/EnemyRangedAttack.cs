using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = false;
        this.CanDepspawnbyhit  = true;
        this.CanDespawnbytime  = true;
    }
}
