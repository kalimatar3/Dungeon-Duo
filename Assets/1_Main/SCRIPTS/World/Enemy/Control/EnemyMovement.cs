using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MyBehaviour
{
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody2D body;
    protected bool facing;
    [SerializeField] protected bool canMove;

    public bool CanMove {get { return canMove;} set {canMove = value;}}
    public float Speed { get {  return speed; } set { speed = value; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy();
        this.LoadRigidbody2D();
    }
    protected void OnEnable() {
        canMove = true;
    }
    protected void LoadRigidbody2D()
    {
        this.body = enemy.GetComponent<Rigidbody2D>();
    }
    protected void LoadEnemy()
    {
        this.enemy = this.GetComponentInParent<Enemy>();
    }
    public void Tracking()
    {
        if(!CanMove) return;
        if(enemy.TarGet != null) enemy.transform.position = Vector3.Lerp(enemy.transform.position,enemy.TarGet.transform.position,Time.deltaTime *1f * speed);
    }
    public void Wandering() {
        if(!CanMove) return;
        // later
    }
    public void FacingtoTarget(Transform target)
    {
        Vector2 Direction = (new Vector3(target.position.x, enemy.transform.position.y) - enemy.transform.position).normalized;
        this.enemy.transform.right = Direction;
    }

}
