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
    protected override void OnEnable()
    {
        base.OnEnable();
        this.hp = enemy.MaxHp;
    }
    public override void Dead()
    {
    }
}
