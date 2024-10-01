using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDameable
{
    public BoxCollider2D Box {  get; set; } 
    public void DeductHp(float dame);
    public void Dead();
    public void IcrHP(float number);
    public void getEffect(Effect effect);
    public Transform GetTransform();
}
