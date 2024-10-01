
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
public class Player : BaseCharacter
{
    [Header("Statistics")]
    [SerializeField] protected float Hp;
    [SerializeField] protected float Mp;
    [SerializeField] protected float Dp;
    [Header("Components")]
    protected Weapon Curweapon;
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
    #region Getter & setter
    public EnemyCheck EnemyCheck {get {return enemyCheck;}}
    public CrossHair CrossHair {get {return crossHair;}}
    public Transform Target { get {return target;} set {target = value;}}
    public PlayerMovement Movement {get { return movement;}}
    public PlayerCollector Collector {get {return collector;}}
    public Weapon Weapon {
        get { return Curweapon;} 
        set {
            Curweapon = value;
            Curweapon.transform.parent = hand;
            Curweapon.transform.localPosition = Vector3.zero;
            this.Curweapon.transform.localRotation = Quaternion.Euler(0,0,0);
        }
    }
    public PlayerEquipment Equipment {get {return equipment;}}
    #endregion
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadeneycheck();
        this.Loadmovement();
        this.LoadEquipment();
        this.statemachine = new PlayerStatemachine();
        this.idleState = new PlayerIdleState(this,statemachine);
        this.detectedState = new PlayerDetectedState(this,statemachine);
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
        StartCoroutine(this.CrSpawn());
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
        if(Curweapon == null) return;
       Curweapon.Attack();
    }
    public virtual void Interact() {
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
