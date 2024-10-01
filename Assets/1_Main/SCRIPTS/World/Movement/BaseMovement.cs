using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : MyBehaviour
{
    [SerializeField] protected BaseCharacter character;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacter();
    }
    protected void LoadCharacter() {
        this.character = GetComponentInParent<BaseCharacter>();
    }
    public abstract void FacingtoTarget(Transform target);
    public abstract void Move();
}
