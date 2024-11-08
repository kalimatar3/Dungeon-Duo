using DG.Tweening;
using UnityEngine;
public abstract class Weapon : BaseItem
{
    [Header("Data")]
    [SerializeField] protected WeaponSO SO;
    [Header("Statisitcs")]
    [SerializeField] protected float dame;
    [SerializeField] protected float firerate;
    [SerializeField] protected float range;
    [Header("variables")]
    public WeaponStateEnum weaponStateEnum;
    public int level = 0; 
    #region Getter & Setter
    public float Dame {get {return dame;}}
    public float Firerate {get {return firerate;}}
    public float Range {get {return range;} }
    public WeaponSO Data {get {return SO;}}
    #endregion
    protected override void OnEnable() {
        base.OnEnable();
        this.level = 0;
        this.dame = SO.Dame;
        this.firerate = SO.Firerate;
        this.range = SO.Range;
        this.UpgradeFormula(level);
    }
    public abstract void AttackScheme();
    public virtual void OnEquip(Player player) {
        this.weaponStateEnum = WeaponStateEnum.OnEquip;
        Debug.Log(player.name +  "Equip " + this.transform.name);
        this.transform.parent = player.Hand;
        this.model.transform.DOKill();
        this.transform.localPosition = Vector3.zero;
        this.model.gameObject.SetActive(true);
        this.transform.localRotation = Quaternion.Euler(0,0,0);     
    }
    public void OnCollect(Player player)
    {
        Debug.Log(player.name + "Collect " + this.transform.name);
        this.weaponStateEnum = WeaponStateEnum.OnCollect;
        this.transform.parent = player.Equipment.transform;
        this.model.transform.localPosition = Vector3.zero;
        this.transform.localPosition = Vector3.zero;
        this.model.gameObject.SetActive(false);
        this.Box.enabled = false;
    }
    public override void OnDrop()
    {
        base.OnDrop();
        this.weaponStateEnum = WeaponStateEnum.OnDrop;
    }
    public override void OnInteract(Player player)
    {
        if(!player) return;
        player.GetComponent<Player>().Equipment.CollectWeapon(this);    
        player.GetComponent<Player>().Equipment.EquipWeapon();
    }
    public abstract void UpgradeFormula(int level);  
    public int GetUpgradePricebylevel() {
        return 100 + level * 50;
    }
    public bool CanUpgradeItem() {
        return MapManager.Instance.Goldnumber >= this.GetUpgradePricebylevel(); 
        
    }
    public void Upgrade() {
        MapManager.Instance.Goldnumber -= GetUpgradePricebylevel();
        this.level ++;
        this.UpgradeFormula(level);
    }

}
public enum WeaponStateEnum {
    OnDrop,
    OnEquip,
    OnCollect,
}
