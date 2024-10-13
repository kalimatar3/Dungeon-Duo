using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MyBehaviour
{
    [SerializeField] RaycastHit2D CheckCircle;
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected bool check;
    [SerializeField] protected LayerMask CharacterLayer;
    public bool Check { get { return check; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    protected void LoadPlayer()
    {
        this.enemy = GetComponentInParent<Enemy>();
    }
    protected void FixedUpdate()
    {
        CheckCircle = Physics2D.CircleCast(this.transform.position,enemy.DetectionRange, Vector2.up, 0f, CharacterLayer); 
        if(CheckCircle) {
            Vector3 dir = ((Vector3)CheckCircle.point - this.transform.position).normalized;
            float distance = ((Vector3)CheckCircle.point - this.transform.position).magnitude;
            RaycastHit2D wallhit = Physics2D.Raycast(this.transform.position,dir,distance,LayerMask.GetMask("Wall"));
            if(!wallhit) 
            {
                enemy.Isdetecting = true;
                enemy.TarGet = CheckCircle.transform.GetComponent<Player>();
                check = true;
            }
        }
        else 
        {
            check = false;
            enemy.TarGet = null;
        }
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,enemy.DetectionRange);
    }
}
