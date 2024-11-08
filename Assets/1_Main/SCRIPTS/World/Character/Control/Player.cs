
using System;
using System.Collections;
using DG.Tweening.Plugins.Options;
using UnityEngine;
public abstract class Player : LazySingleton<Player>,IDespawnable
{
    [Header("Statistics")]
    [SerializeField] protected CharacterSO playerSO;
    
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
    [SerializeField] protected float skillCooldownTimer;
    [SerializeField] protected float SkillMpCost;
    protected float maxMp,curMp;
    protected float maxDp;
    protected float maxHp;
    protected int level;
    [SerializeField] protected float RecoverCombatSateTime = 10f;
    [SerializeField] protected float RestoreoneDPtime = 0.2f,RestoreoneDPtimer = 0;
    [SerializeField] protected bool iscombatstate;
    public Vector2 minnoticebox;
    [SerializeField] protected LayerMask EnemyLayer;
    [Header("Corotine")]
    protected Coroutine SkillCoolDownCr;
    protected Coroutine AttackCoolDownCr;
    [Header("UI")]
    public GamePlay_Panel UI;
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
    public int Level {get {return level;} set {level = value;}}
    public CharacterSO SO {get {return playerSO;}}
    public float SkillCooldownTimer {get {return skillCooldownTimer;}}
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
        this.CalculateStats(level); 
        StartCoroutine(this.CrDelayLoadNoticeBox());
        StartCoroutine(this.CrDeLayLoadUI());
    }
    protected IEnumerator CrDeLayLoadUI() {
        yield return new WaitUntil(()=> {
            if(!PanelManager.instance.getPanelbyName("GamePlay_Panel")) return false;
            return true;
        });
        UI = PanelManager.instance.getPanelbyName("GamePlay_Panel").GetComponent<GamePlay_Panel>();
    }
    protected IEnumerator CrDelayLoadNoticeBox() {
        yield return new WaitUntil(predicate:()=> {
            if(CameraManager.Instance == null) return false;
            return true;
        });
        noticeBox = new Vector2(CameraManager.Instance.ScreenWidth,CameraManager.Instance.ScreenLength);   
    }
    protected abstract void CalculateStats(int level);
    #endregion
    public virtual void Attack() {
        if(!canattack) return;
        if(Curweapon == null) return;
        if(AttackCoolDownCr != null) StopCoroutine(AttackCoolDownCr);
        AttackCoolDownCr = StartCoroutine(CrCoolDownAttack());
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
        if(curMp < SkillMpCost) return;
        curMp -= SkillMpCost;
        if(SkillCoolDownCr != null) StopCoroutine(SkillCoolDownCr);
        SkillCoolDownCr = StartCoroutine(this.CrCoolDownSkill());
        this.SkillScheme();
    }
    protected IEnumerator CrCoolDownSkill() {
        this.candoskill = false;
        skillCooldownTimer = skillCooldownTime;
        while (skillCooldownTimer > 0) {
            skillCooldownTimer -= Time.deltaTime *1f;
            UI.playerSkill_Button.playerSkillCoolDown_Slider.SetvalueSlider(skillCooldownTimer/skillCooldownTime);
            yield return new WaitForFixedUpdate();
        } 
        this.candoskill = true;
    }
    public void IcrMp(float value) {
        this.curMp += value;
        if(curMp > maxMp) curMp = maxMp;
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
        if(Target != null && Target.gameObject.activeInHierarchy) {
            this.statemachine.ChangeSate(detectedState);
        }
        else {
            this.statemachine.ChangeSate(idleState);
        }
        this.statemachine.CurState.FrameUpdate();
        Vector3 Direction = (crossHair.transform.position - this.transform.position).normalized;
        hand.transform.up = Direction;
    }
    public void RestoreDP() {
        if(iscombatstate) 
        {
            this.RestoreoneDPtimer = 0;
            return;
        }
        if(reciver.Dp >= this.maxDp) 
        {
            this.RestoreoneDPtimer = 0;
            return;
        }
        RestoreoneDPtimer += Time.deltaTime *1f;
        if(RestoreoneDPtimer >= RestoreoneDPtime) {
            RestoreoneDPtimer = 0;
            this.reciver.Dp++;
        }
    }
    public Coroutine SetcombatStateCr;
    public IEnumerator CrSetCombatState() {
        this.iscombatstate = true;
        yield return new WaitForSeconds(RecoverCombatSateTime);
        this.iscombatstate = false;
    }
    public virtual float GetLevelupPricebyLevel(float level) {
        return 1000 * (1 + level * 250);
    }
    protected void FixedUpdate() {
        this.GetTarget();
        this.ChangeState();
        this.RestoreDP();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, NoticeBox);
    }
    public void DeSpawn()
    {
        ScenesManager.instance.LoadScenebyName(MyScene.MainMenuScene.ToString());
    }
}
