using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Potion : BaseItem
{
    [SerializeField] protected bool Dropdone;
    protected override void OnEnable()
    {
        StartCoroutine(this.CrDelayDrop());
    }
    protected IEnumerator CrDelayDrop() {
        this.Dropdone = false;
        Vector3 DropPos = new Vector3(Random.Range(1f,2f),Random.Range(1f,2f));
        this.transform.DOMove(this.transform.position + DropPos,1f);
        yield return new WaitForSeconds(1f);
        this.Dropdone = true;
        this.OnDrop();
    }
}
