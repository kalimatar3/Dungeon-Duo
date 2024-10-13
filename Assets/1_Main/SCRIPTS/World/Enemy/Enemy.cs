using System;
using System.Collections;
using System.Net.Security;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MyBehaviour,IDespawnable
{
    [Header("Data")]
    [Space(5f)]
    [SerializeField] protected EnemySO enemySO;
    [Header("variables")]
    protected float detectionRange;
    protected float attackRange;
    protected float firerate;
    protected float timer;
    protected bool canAttack = true;
    [SerializeField] protected bool candetected;
    [SerializeField] protected bool isdetecting;
    [Header("Components")]
    [Space(5f)]
    [SerializeField] protected Room roomholder;
    [SerializeField] protected EnemyMovement movement;
    [SerializeField] protected EnemyReciver reciver;
    [SerializeField] protected PlayerCheck playerCheck;
    [SerializeField] protected Player target;
    [SerializeField] protected LayerMask WallLayer;
    [SerializeField] protected LayerMask PlayerLayer;
    [Header("StateMachine")]
    protected EnemyStateMachine stateMachine;
    protected EnemyWanderingState wanderingState;
    protected EnemyTrackingState trackingState;
    protected EnemyAttackState attackState;
    protected EnemyDeathState deathState;
    protected EnemyWalledState walledState;
    
    #region Getter & setter
    public bool CanDetected {get {return candetected;} 
        set {
            candetected = value;
            stateMachine.CanChange = value;
        }
    }
    public float Timer { get {  return timer; } set { timer = value; } }
    public float Firerate {  get { return firerate; } }
    public float DetectionRange { get { return detectionRange; }}
    public float AttackRange {get {return attackRange;}}
    public bool CanAttack {get {return canAttack;} set {canAttack = value;}}
    public bool Isdetecting {get {return isdetecting;} set {isdetecting = value;}}
    public Player TarGet {get {return target;} set {target = value;}}
    public EnemyMovement Movement {get {return movement;}}
    public EnemyStateMachine StateMachine {get {return stateMachine;}}
    public EnemyWanderingState WanderingState {get {return wanderingState;}}
    public EnemyReciver Reciver {get {return reciver;}}
    public Room RoomHolder {get {return roomholder;} set {roomholder = value;}}
    public EnemySO SO {get {return enemySO;}}
    #endregion
    #region Loadcomponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadReciver();
        this.LoadCharacterCheck();
        this.loadMovement();
        this.LoadData();
    }
    protected void LoadData() {
        this.firerate = enemySO.FireRate;
        this.detectionRange = enemySO.DetectionRange;
        this.attackRange = enemySO.AttackRange;
        this.movement.Speed = enemySO.Wandaspeed;
    }
    protected void loadMovement() {
        movement = GetComponentInChildren<EnemyMovement>();
    }
    protected void LoadReciver() {
        this.reciver = GetComponentInChildren<EnemyReciver>();
    }
    protected void LoadCharacterCheck() {
        this.playerCheck = GetComponentInChildren<PlayerCheck>();
    }
    #endregion
    public abstract void Attack();
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        stateMachine.CanChange = false;
        wanderingState = new EnemyWanderingState(stateMachine,this);
        trackingState = new EnemyTrackingState(stateMachine,this);
        attackState = new EnemyAttackState(stateMachine,this);
        deathState = new EnemyDeathState(stateMachine,this);
        walledState = new EnemyWalledState(stateMachine,this);
    }
    protected virtual void OnEnable() {
        this.stateMachine.Initialize(wanderingState);
        this.candetected  = false;
        this.reciver.Hp = enemySO.MaxHp;
    }
    protected virtual void UpdateState() {
        this.stateMachine.CurState.FrameUpdate();
        RaycastHit2D circlecheck = Physics2D.CircleCast(this.transform.position,attackRange,Vector2.up,0f,PlayerLayer);
        if(target != null) {
            Vector3 dir = (TarGet.transform.position - this.transform.position).normalized;
            float distance = (TarGet.transform.position - this.transform.position).magnitude;
            RaycastHit2D wallcirclehit = Physics2D.CircleCast(this.transform.position,2f,Vector2.up,0f,WallLayer);
            RaycastHit2D wallhit = Physics2D.Raycast(this.transform.position,dir,distance,WallLayer);
            if(wallcirclehit) {
                stateMachine.ChangeSate(walledState);
                return;   
            }
            else if(circlecheck && !wallhit) {
                stateMachine.ChangeSate(attackState);
                return;
            }
            else {
                stateMachine.ChangeSate(trackingState);
            }
        }
        else {
            this.stateMachine.ChangeSate(wanderingState);
        }
    }
    protected virtual void FixedUpdate() {
        this.UpdateState();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
    public void DeSpawn()
    {
        EnemySpawner.Instance.DeSpawnToPool(this.transform);
    }
}
