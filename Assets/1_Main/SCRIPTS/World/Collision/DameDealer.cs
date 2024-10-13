using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public abstract class DameDealer : MyBehaviour, IHitable
{
    [SerializeField] public Collider2D Box { get; set; }
    [SerializeField] protected float dame;
    [SerializeField] protected bool CanDeal = true;
    [SerializeField] protected bool Multicalhit = false;
    public float Dame { get {  return dame; } set { dame = value; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider();
    }
    protected virtual void LoadBoxCollider()
    {
        this.Box = GetComponent<Collider2D>();
        this.Box.isTrigger = true;
    }
    protected virtual void OnEnable() {
        this.CanDeal = true;
    }
    public virtual void Hit(IDameable dameable)
    {
        if(!CanDeal) return;
        if(!Multicalhit) CanDeal = true;
        this.DealDame(this.dame,dameable);
    }
    protected virtual void DealDame(float dame,IDameable dameable) {
        dameable.DeductHp(dame);
        Debug.Log(this.transform.name + " Deal "  + dame + " to " + dameable.GetTransform().name);        
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IDameable>() != null)
        {
            this.Hit(collision.GetComponent<IDameable>());
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<IDameable>() != null)
        {
            this.Hit(collision.transform.GetComponent<IDameable>());
        }
    }
    public virtual void DoEffect(IDameable dameable)
    {
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
}
