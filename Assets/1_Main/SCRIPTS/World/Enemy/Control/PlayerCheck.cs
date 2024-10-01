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
            enemy.TarGet = CheckCircle.transform.GetComponent<Player>();
            check = true;
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
