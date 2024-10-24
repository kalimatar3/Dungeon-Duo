using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
[RequireComponent(typeof(Collider2D))]
public abstract class BaseItem : MyBehaviour,Iinteractable
{
    [Header("Components")]
    [SerializeField] protected SpriteRenderer model;
    [SerializeField] protected BoxCollider2D box;
    #region getter && setter
    public SpriteRenderer Model {get {return model;}}
    public BoxCollider2D Box {get {return box;}}
    #endregion
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadmodel();
        this.Loadbox();
    }
    protected void Loadbox() {
        this.box = GetComponent<BoxCollider2D>();
        this.box.isTrigger = true;
    }
    protected void Loadmodel() {
        this.model = GetComponentInChildren<SpriteRenderer>();
    }
    protected virtual void OnEnable() {
        this.OnDrop();
    }
    public virtual void OnCollect(Player character) {
        this.model.gameObject.SetActive(false);
        this.box.enabled = false;
    }
    public void OnDrop() {
        this.transform.parent = null;
        this.model.gameObject.SetActive(true);
        this.box.enabled = true;
        this.model.transform.up = Vector2.up;
        this.model.transform.DOMoveY(this.model.transform.position.y + 0.3f ,0.5f).SetLoops(-1,LoopType.Yoyo);
    }
    public virtual void OnInteract(Player player)
    {
        this.OnCollect(player);
    }
    public Transform GetTransform()
    {
       return this.transform;
    }
}

