using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyDealer : DameDealer, IDespawnable
{   
    [SerializeField] protected bool CanDespawnbytime = true,
    CanDepspawnbyhit = true; 
    [SerializeField] protected float lifetime;
    protected override void OnEnable() {
        base.OnEnable();
        if(CanDespawnbytime) this.StartCoroutine(this.CrDespawn());
    }
    protected virtual IEnumerator CrDespawn() {
        yield return new WaitForSecondsRealtime(lifetime);
        this.DeSpawn();
    }
    public override void Hit(IDameable dameable)
    {
        if(dameable.GetType() == typeof(EnemyReciver)) return;
        base.Hit(dameable);
        if(CanDepspawnbyhit) this.DeSpawn();
    }
    public void DeSpawn()
    {
        EnemySchemeSpawner.Instance.DeSpawnToPool(this.transform);
    }
}