using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FollowMovement : BaseMovement
{
    public bool canfollow  = false;
    public Vector3 Direction;
    protected void OnEnable() {
        this.StartCoroutine(this.FirstMove());
    }
    protected void FixedUpdate() {
        this.MoveScheme();
    }
    public override void MoveScheme()
    {
        if(!canfollow) return;
        this.transform.parent.position += Direction * speed * Time.deltaTime;
    }
    protected IEnumerator FirstMove() {
        Vector3 firstDes = this.transform.parent.position + new Vector3(Random.Range(-5,6), Random.Range(0,6));
        this.canfollow = false;
        this.transform.parent.DOMove(firstDes,1.5f);
        yield return new WaitForSeconds(3f);
        this.canfollow = true;
        Direction = (Player.Instance.transform.position - this.transform.position).normalized;
    }
}
