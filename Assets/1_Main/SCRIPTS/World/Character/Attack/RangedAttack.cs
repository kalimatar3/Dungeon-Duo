using UnityEngine;

public class RangedAttack : PlayerDealer 
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Multicalhit = false;
        this.canDespawnbytime  = true;
        this.canDeSpawnbyhit = true;
    }
    public override void DoEffect(IDameable dameable)
    {
        base.DoEffect(dameable);
        dameable.Knockback(this,1);
    }
}