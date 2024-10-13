using UnityEngine;

public class EnemyReciver : DameReciver
{
    [SerializeField] protected Enemy enemy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadenemy();
    }
    protected void Loadenemy()
    {
        this.enemy = this.GetComponentInParent<Enemy>();
    }
    public override void Dead()
    {
        base.Dead();
        this.enemy.DeSpawn();
    }
}
