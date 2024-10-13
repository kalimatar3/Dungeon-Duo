using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public Collider2D Box { get; set; }
    public void Hit(IDameable dameable);
    public void DoEffect(IDameable dameable);
    public Transform GetTransform();
}
