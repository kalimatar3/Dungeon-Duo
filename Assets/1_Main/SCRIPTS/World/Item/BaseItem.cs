using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Collider2D))]
public abstract class BaseItem : MyBehaviour,Iinteractable
{
    [Header("Data")]
    [SerializeField] protected string Name;
    [SerializeField] protected Sprite sprite;
    [Header("Components")]
    [SerializeField] protected SpriteRenderer model;
    [SerializeField] protected BoxCollider2D box;
    #region getter && setter
    public string ItemName {get {return Name;}}
    #endregion
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadmodel();
        this.Loadbox();
    }
    protected void Loadbox() {
        this.box = GetComponent<BoxCollider2D>();
    }
    protected void Loadmodel() {
        this.model = GetComponentInChildren<SpriteRenderer>();
        this.model.sprite = this.sprite;
    }
    protected void OnEnable() {
        this.OnDrop();
    }
    public virtual void OnCollect(BaseCharacter character) {
        this.model.transform.DOKill();
        this.model.gameObject.SetActive(false);
        this.box.enabled = false;
    }
    public void OnDrop() {
        this.model.transform.DOMoveY(this.model.transform.position.y + 0.3f ,0.5f).SetLoops(-1,LoopType.Yoyo);
    }
    public void OnInteract(Player player)
    {
        this.OnCollect(player);
    }
    public Transform GetTransform()
    {
       return this.transform;
    }
}

