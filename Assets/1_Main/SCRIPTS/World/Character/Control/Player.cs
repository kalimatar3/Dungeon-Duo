
using System;
using System.Collections;
using UnityEngine;
public abstract class Player : LazySingleton<Player>,IDespawnable
{
    [Header("Statistics")]
    [SerializeField] protected CharacterSO characterSO;
    
    [Header("Components")]
    protected Weapon Curweapon;
    [SerializeField] protected Transform hand;
    [SerializeField] protected PlayerReciver reciver;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected PlayerMovement movement;
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
    [SerializeField] protected bool canattack = true;
    [SerializeField] protected bool caninteract = true;
    [SerializeField] protected bool candoskill = true;
    [SerializeField] protected float skillCooldownTime;
    protected float SkillMpCost;
    protected float maxMp,curMp;
    protected float maxDp;
    protected float maxHp;
    protected float level;
    protected Vector2 minnoticebox;
    [SerializeField] protected LayerMask EnemyLayer;
    #region Getter & setter
    public Transform Hand {get {return hand;}}
    public PlayerReciver Reciver { get {return reciver;}}
    public Vector2 NoticeBox {get {return noticeBox;}}
    public bool Canattack {get {return canattack;} set {canattack = value;}}
    public bool Caninteract {get {return caninteract;} set {caninteract = value;}}
    public CrossHair CrossHair {get {return crossHair;}}
    public Transform Target { get {return target;} set {target = value;}}
    public PlayerMovement Movement {get { return movement;}}
    public PlayerCollector Collector {get {return collector;}}
    public Rigidbody2D Body {get {return body;}}
    public float Level {get {return level;} set {level = value;}}
    public CharacterSO SO {get {return characterSO;}}
    public float CurMp {get {return curMp;}}
    public float MaxMp {get {return maxMp;} 
                        set {
                                maxMp = value;
                                curMp = maxMp;
                            }}
    public float MaxDp {get {return maxDp;}
                        set {
                                maxDp = value;
                                this.reciver.Dp = maxDp;
                            }
                        }
    public float MaxHp {
        get {return maxHp;}
        set {
                maxHp = value;
                this.reciver.Hp = maxHp;
            }
    }
    public Weapon Weapon {
        get { return Curweapon;} 
        set {
            Curweapon = value;
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
        this.LoadCrossHair();
        this.Loadmovement();
        this.LoadEquipment();
        this.LoadBody();
        this.LoadCollector();
        this.LoadReciver();
        this.Loadhand();
    }
    protected void LoadCollector() {
        this.collector = GetComponentInChildren<PlayerCollector>();
    }
    protected void LoadCrossHair() {
        this.crossHair = GetComponentInChildren<CrossHair>();
    }
    protected void Loadhand() {
        this.hand = this.transform.Find("Hand");
    }
    protected void LoadReciver()
    {
       reciver = GetComponentInChildren<PlayerReciver>();
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
    #endregion
    #region LoadData
    protected virtual void OnEnable() {
        this.statemachine.Initialize(idleState);
        this.CalculateStats(1); 
        StartCoroutine(this.CrDelayLoadNoticeBox());
    }
    protected IEnumerator CrDelayLoadNoticeBox() {
        yield return new WaitUntil(predicate:()=> {
            if(CameraManager.Instance == null) return false;
            return true;
        });
        noticeBox = new Vector2(CameraManager.Instance.ScreenWidth,CameraManager.Instance.ScreenLength);   
    }
    public IEnumerator CrSpawn() {
        yield return new WaitUntil(predicate:()=> {
            if(MapManager.Instance == null) return false;
            if(MapManager.Instance.Rooms.Count <= 0) return false;
            return true;
        });
        this.transform.position = MapManager.Instance.Rooms[0].transform.position;      
    }
    protected abstract void CalculateStats(int level);
    #endregion
    public virtual void Attack() {
        if(!canattack) return;
        if(Curweapon == null) return;
        StartCoroutine(CrCoolDownAttack());
        Curweapon.AttackScheme();
    }
    protected IEnumerator CrCoolDownAttack() {
        this.canattack = false;
        yield return new WaitForSeconds(Weapon.Firerate);
        this.canattack = true;
    }
    public virtual void Interact() {
        if(!caninteract) return;
        if(this.collector.ListInteractable != null) this.collector.ListInteractable.OnInteract(this);
    }
    protected abstract void SkillScheme();
    public void DoSkill() {
        if(!candoskill) return;
        if(maxMp < SkillMpCost) return;
        maxMp -= SkillMpCost;
        StartCoroutine(this.CrCoolDownSkill());
        this.SkillScheme();
    }
    protected IEnumerator CrCoolDownSkill() {
        this.candoskill = false;
        yield return new WaitForSeconds(this.skillCooldownTime);
        this.candoskill = true;
    }
    protected void GetTarget()
    {
        RaycastHit2D boxcheck = Physics2D.BoxCast(this.transform.position,NoticeBox,0f,Vector2.up,0,EnemyLayer);
        if(boxcheck) {
            if((boxcheck.transform.position - this.transform.position).x <= minnoticebox.x && (boxcheck.transform.position - this.transform.position).y <= minnoticebox.y ) 
            {
                this.minnoticebox = boxcheck.transform.position - this.transform.position;
                this.Target = boxcheck.transform;
            }
        }
        else 
        {
            Target = null;
            this.minnoticebox = NoticeBox;
        }
    }
    protected virtual void ChangeState() {
        if(Target != null) {
            this.statemachine.ChangeSate(detectedState);
        }
        else {
            this.statemachine.ChangeSate(idleState);
        }
        this.statemachine.CurState.FrameUpdate();
        Vector3 Direction = (crossHair.transform.position - this.transform.position).normalized;
        hand.transform.up = Direction;
    }
    public void Levelup() {
    }
    protected void FixedUpdate() {
        this.GetTarget();
        this.ChangeState();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, NoticeBox);
    }

    public void DeSpawn()
    {
       this.reciver.StopAllCoroutines();
    }
}
