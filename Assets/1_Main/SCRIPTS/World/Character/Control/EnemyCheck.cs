using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class EnemyCheck : MyBehaviour
{
    [SerializeField] protected Player character;
    [SerializeField] protected bool check;
    [SerializeField] protected LayerMask EnemyLayer;
    protected float minDistance = 10f;
    [SerializeField] protected float DetectionRange = 10f;
    public bool Check { get {  return check;} } 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    protected void LoadPlayer()
    {
        this.character = GetComponentInParent<Player>();
    }
    protected void OnEnable() {
        this.minDistance = DetectionRange;
    }
    protected void FixedUpdate()
    {
        RaycastHit2D circleCheck = Physics2D.CircleCast(this.transform.position,DetectionRange,Vector2.up,0,EnemyLayer);
        if(circleCheck) {
            if((circleCheck.transform.position - this.transform.position).magnitude <= minDistance) {
                this.minDistance = (circleCheck.transform.position - this.transform.position).magnitude;
                this.character.Target = circleCheck.transform;
            }
        }
        else 
        {
            character.Target = null;
            minDistance = DetectionRange;
        }
        if(character.Target) check = true;
        else check = false; 
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }

}
