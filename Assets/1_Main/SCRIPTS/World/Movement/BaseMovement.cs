using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : MyBehaviour
{
    [SerializeField] protected float speed = 5;
    public float Speed {get {return speed;} set {speed = value;}}
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    public abstract void MoveScheme();
}
