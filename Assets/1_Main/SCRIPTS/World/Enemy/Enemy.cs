using System.Collections;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MyBehaviour
{
    [Header("Data")]
    [Space(5f)]
    [SerializeField] protected float maxHP;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float firerate;
    protected float timer;
    protected bool canAttack = true;

    [Header("Components")]
    [Space(5f)]
    [SerializeField] protected EnemyMovement movement;
    [SerializeField] protected EnemyReciver reciver;
    [SerializeField] protected PlayerCheck playerCheck;
    [SerializeField] protected Player target;
    [SerializeField] protected LayerMask PlayerLayer;
    [SerializeField] protected PlayerEquipment equipment;

    [Header("StateMachine")]
    protected EnemyStateMachine stateMachine;
    protected EnemyWanderingState wanderingState;
    protected EnemyTrackingState trackingState;
    protected EnemyAttackState attackState;
    
    #region Getter & setter
    public float Timer { get {  return timer; } set { timer = value; } }
    public float Firerate {  get { return firerate; } }
    public float DetectionRange { get { return detectionRange; } }
    public float MaxHp {get {return maxHP;} set {maxHP = value;}}
    public bool CanAttack {get {return canAttack;} set {canAttack = value;}}
    public Player TarGet {get {return target;} set {target = value;}}
    public EnemyMovement Movement {get {return movement;}}
    public PlayerEquipment Equipment {get {return equipment;}}
    public EnemyStateMachine StateMachine {get {return stateMachine;}}
    public EnemyReciver Reciver {get {return reciver;}}
    #endregion
    #region Loadcomponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStat();
        this.LoadReciver();
        this.LoadCharacterCheck();
    }
    protected void LoadReciver() {
        this.reciver = GetComponentInChildren<EnemyReciver>();
    }
    protected void LoadCharacterCheck() {
        this.playerCheck = GetComponentInChildren<PlayerCheck>();
    }
    /// <summary>
    /// Firerate = ?
    /// AttackRange = ?
    /// DetectionRange = ?
    /// Max Hp = ?
    /// </summary>
    protected abstract void LoadStat();
    #endregion
    public abstract void Attack();
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        wanderingState = new EnemyWanderingState(stateMachine,this);
        trackingState = new EnemyTrackingState(stateMachine,this);
        attackState = new EnemyAttackState(stateMachine,this);
    }
    protected virtual void OnEnable() {
        this.stateMachine.Initialize(wanderingState);
    }
    protected virtual void FixedUpdate() {
        this.stateMachine.CurState.FrameUpdate();
        RaycastHit2D circlecheck = Physics2D.CircleCast(this.transform.position,attackRange,Vector2.up,0f,PlayerLayer);
        if(circlecheck) {
            stateMachine.ChangeSate(attackState);
        }
        else {
            if(playerCheck.Check) {
                stateMachine.ChangeSate(trackingState);
            }
            else {
                stateMachine.ChangeSate(wanderingState);
            }
        }
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}
