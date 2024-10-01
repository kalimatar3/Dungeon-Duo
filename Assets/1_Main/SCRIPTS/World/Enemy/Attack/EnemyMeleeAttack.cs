using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = true;
        this.CanDepspawnbyhit  = true;
        this.CanDespawnbytime  = true;
    }
}
