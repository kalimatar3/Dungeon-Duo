using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemyMovement : MyBehaviour
{
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected float speed;
    [SerializeField] protected bool facing;
    [Header("Statemachine")] 
    [SerializeField] protected bool canMove;
    public bool CanMove {get { return canMove;} set {canMove = value;}}
    public float Speed { get {  return speed; } set { speed = value; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy();
    }
    protected void OnEnable() {
        canMove = true;
    }
    protected void LoadEnemy()
    {
        this.enemy = this.GetComponentInParent<Enemy>();
    }
    public void Tracking()
    {
        if(!CanMove) return;
        if(enemy.TarGet == null) return;
        Vector3 dir = (enemy.TarGet.transform.position - enemy.transform.position).normalized;
        enemy.transform.position += dir * speed * Time.deltaTime;
    }
    public void MovetoPosition(Vector3 position) {
        Vector3 dir = (position - enemy.transform.position).normalized;
        enemy.transform.position += dir * speed * Time.deltaTime;
    }
    public void Wandering() {
        if(!CanMove) return;
    }
    public void FacingtoTarget(Transform target)
    {
        if(!facing) return;
        Vector2 Direction = (new Vector3(target.position.x, enemy.transform.position.y) - enemy.transform.position).normalized;
        this.enemy.transform.right = Direction;
    }

}
