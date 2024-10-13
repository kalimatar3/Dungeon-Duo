using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Collider2D))]
public class PlayerReciver : DameReciver
{
    [SerializeField] protected Player player;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    protected void LoadPlayer() {
        this.player = GetComponentInParent<Player>();
    }
    public override void Stun(float time)
    {
        base.Stun(time);
        StartCoroutine(CrStun(time));
    }
    protected IEnumerator CrStun(float time) {
        player.Canattack = false;
        player.Caninteract = false;
        yield return new WaitForSeconds(time);
        player.Canattack = true;
        player.Caninteract = true;
    }
    public override void Knockback(IHitable hitable, float power)
    {
        base.Knockback(hitable, power);
        Debug.Log("knockback");
        Vector3 dir = (transform.position - new Vector3(hitable.GetTransform().position.x,transform.position.y,this.transform.position.z)).normalized;
        player.Body.velocity =  Vector2.zero;
        player.Body.AddForce(dir * power, ForceMode2D.Impulse);
        StartCoroutine(CrStopKnockBack());  
    }
    protected IEnumerator CrStopKnockBack() {
        yield return new WaitForSeconds(0.5f);
        player.Body.velocity = Vector2.zero;
    }
}
