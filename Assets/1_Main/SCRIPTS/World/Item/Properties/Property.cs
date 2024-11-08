using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class Property : BaseItem
{
    public int Value = 10;
    [SerializeField] protected float CollectableRadius = 5f;
    [SerializeField] protected Player player1;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected bool Dropdone;
    protected override void OnEnable()
    {
        this.player1 = null;
        StartCoroutine(this.CrDelayDrop());
    }
    protected IEnumerator CrDelayDrop() {
        this.Dropdone = false;
        Vector3 DropPos = new Vector3(Random.Range(1f,2f),Random.Range(1f,2f));
        this.transform.DOMove(this.transform.position +DropPos,1f);
        yield return new WaitForSeconds(1f);
        this.Dropdone = true;
        this.OnDrop();
    }
    protected void FixedUpdate() {
        if(!Dropdone) return;
        if(player1 != null) {
            this.model.transform.DOKill();
            Vector3 dir = (player1.transform.position - this.transform.position).normalized;
            this.transform.position += dir *speed * Time.deltaTime;
            return;
        }
        RaycastHit2D circlehit = Physics2D.CircleCast(this.transform.position,CollectableRadius,Vector2.up,0f,LayerMask.GetMask("Player"));
        if(circlehit) {
            if(circlehit.transform.GetComponent<Player>()) {
                player1 = circlehit.transform.GetComponent<Player>();
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position,CollectableRadius);    
    }
}