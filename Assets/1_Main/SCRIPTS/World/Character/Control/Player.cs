
using System.Collections;
using UnityEngine;
public class Player : BaseCharacter
{
    [Header("Statistics")]
    [SerializeField] protected float Hp;
    [SerializeField] protected float Mp;
    [SerializeField] protected float Dp;
    [SerializeField] protected float NoticeRange = 1;
    
    [Header("Components")]
    protected Weapon Curweapon;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected PlayerMovement movement;
    [SerializeField] protected EnemyCheck enemyCheck;
    [SerializeField] protected Transform target;
    [SerializeField] protected CrossHair crossHair;
    [SerializeField] protected PlayerEquipment equipment;
    [SerializeField] protected PlayerCollector collector;
    [Header(" StateMachine")]
    protected PlayerStatemachine statemachine;
    protected PlayerIdleState idleState;
    protected PlayerDetectedState detectedState;
    [Header("Variables")]
    [SerializeField] protected Vector2 noticeBox;
    protected bool canattack;
    protected bool caninteract;
    #region Getter & setter
    public Vector2 NoticeBox {get {return noticeBox;}}
    public bool Canattack {get {return canattack;} set {canattack = value;}}
    public bool Caninteract {get {return caninteract;} set {caninteract = value;}}
    public EnemyCheck EnemyCheck {get {return enemyCheck;}}
    public CrossHair CrossHair {get {return crossHair;}}
    public Transform Target { get {return target;} set {target = value;}}
    public PlayerMovement Movement {get { return movement;}}
    public PlayerCollector Collector {get {return collector;}}
    public Rigidbody2D Body {get {return body;}}
    public Weapon Weapon {
        get { return Curweapon;} 
        set {
            if(Curweapon) {
                equipment.EquipWeapon(Curweapon);         
            }
            Curweapon = value;
            Curweapon.transform.parent = hand;
            Curweapon.transform.localPosition = Vector3.zero;
            Curweapon.Model.gameObject.SetActive(true);
            this.Curweapon.transform.localRotation = Quaternion.Euler(0,0,0);
        }
    }
    public PlayerEquipment Equipment {get {return equipment;}}
    #endregion
    #region LoadComponents
    protected override void Awake()
    {
        base.Awake();
        this.statemachine = new PlayerStatemachine();
        this.idleState = new PlayerIdleState(this,statemachine);
        this.detectedState = new PlayerDetectedState(this,statemachine);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadeneycheck();
        this.Loadmovement();
        this.LoadEquipment();
        this.LoadBody();
    }
    protected void LoadBody() {
        this.body = GetComponent<Rigidbody2D>();
    }
    protected void LoadEquipment() {
        this.equipment = GetComponentInChildren<PlayerEquipment>();
    }
    protected void Loadmovement() {
        this.movement = GetComponentInChildren<PlayerMovement>();
    }
    protected void Loadeneycheck() {
        this.enemyCheck = this.GetComponentInChildren<EnemyCheck>();
    }
    #endregion
    protected virtual void OnEnable() {
        this.statemachine.Initialize(idleState);
        this.reciver.Hp = this.Hp;
        StartCoroutine(this.CrDelayLoadNoticeBox());
        StartCoroutine(this.CrSpawn());
    }
    protected IEnumerator CrDelayLoadNoticeBox() {
        yield return new WaitUntil(predicate:()=> {
            if(CameraManager.Instance == null) return false;
            return true;
        });
        noticeBox = new Vector2(CameraManager.Instance.ScreenWidth,CameraManager.Instance.ScreenLength) * NoticeRange;   
    }
    protected IEnumerator CrSpawn() {
        yield return new WaitUntil(predicate:()=> {
            if(MapManager.Instance == null) return false;
            if(MapManager.Instance.Map.Count <= 0) return false;
            return true;
        });
        this.transform.position = MapManager.Instance.Map[0].transform.position;       
    }
    public virtual void Attack() {
        if(!canattack) return;
        if(Curweapon == null) return;
       Curweapon.AttackScheme();
    }
    public virtual void Interact() {
        if(!caninteract) return;
        if(this.collector.ListInteractable != null)
        this.collector.ListInteractable.OnInteract(this);
    }
    protected void FixedUpdate() {
        if(enemyCheck.Check) {
            this.statemachine.ChangeSate(detectedState);
        }
        else {
            this.statemachine.ChangeSate(idleState);
        }
        this.statemachine.CurState.FrameUpdate();
        Vector3 Direction = (crossHair.transform.position - this.transform.position).normalized;
        hand.transform.up = Direction;
    }
}
