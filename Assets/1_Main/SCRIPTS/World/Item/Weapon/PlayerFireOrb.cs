using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFireOrb : RangedAttack
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = true;
        this.canDespawnbytime =true;
        this.canDeSpawnbyhit = false;
    }
}
