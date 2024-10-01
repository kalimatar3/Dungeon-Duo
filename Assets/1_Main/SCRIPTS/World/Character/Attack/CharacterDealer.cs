using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterDealer : DameDealer, IDespawnable
{
    [SerializeField] protected float lifetime;
    protected override void OnEnable() {
        base.OnEnable();
        this.StartCoroutine(this.CrDespawn());
    }
    protected virtual IEnumerator CrDespawn() {
        yield return new WaitForSecondsRealtime(lifetime);
        this.DeSpawn();
    }
    public void DeSpawn()
    {
        WeaponAttackSpawner.Instance.DeSpawnToPool(this.transform);
    }
    public override void Hit(IDameable dameable)
    {
        if(dameable.GetType() == typeof(PlayerReciver)) return;
        base.Hit(dameable);
        this.DeSpawn();
    }
}
