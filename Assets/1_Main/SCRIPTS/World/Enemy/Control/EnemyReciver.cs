using System.Collections;
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
    public override void Knockback(IHitable hitable, float power)
    {
        base.Knockback(hitable, power);
        Debug.Log(enemy.name + " knockback ");
        Vector3 dir = (transform.position - new Vector3(hitable.GetTransform().position.x,transform.position.y,this.transform.position.z)).normalized;
        enemy.Body.velocity =  Vector2.zero;
        enemy.Body.AddForce(dir * power, ForceMode2D.Impulse);
        StartCoroutine(CrStopKnockBack());  
    }
    protected IEnumerator CrStopKnockBack() {
        yield return new WaitForSeconds(0.5f);
        enemy.Body.velocity = Vector2.zero;
    }
}
