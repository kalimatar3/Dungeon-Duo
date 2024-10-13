using UnityEngine;
public interface IDameable
{
    public Collider2D Box {  get; set; } 
    public void DeductHp(float dame);
    public void IcrHP(float number);
    public void Stun(float time);
    public void Knockback(IHitable hitable,float power);
    public Transform GetTransform();
    public void Dead();
}
