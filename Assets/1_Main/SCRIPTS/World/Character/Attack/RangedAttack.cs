using UnityEngine;

public class RangedAttack : CharacterDealer 
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = false;
        this.canDespawnbytime  = true;
        this.canDeSpawnbyhit = true;
    }
}