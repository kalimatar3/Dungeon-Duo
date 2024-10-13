using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class MeleeAttack : CharacterDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = true;
        this.canDeSpawnbyhit = false;
        this.canDespawnbytime = true;
    }
}
