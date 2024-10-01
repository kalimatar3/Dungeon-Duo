using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : CharacterDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = true;
    }
}
