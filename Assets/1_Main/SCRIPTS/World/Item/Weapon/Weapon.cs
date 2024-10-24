using System.Collections;
using System.Collections.Generic;
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
    #region Getter & Setter
    public float Dame {get {return dame;}}
    public float Firerate {get {return firerate;}}
    public float Range {get {return range;} }
    #endregion
    protected override void OnEnable() {
        base.OnEnable();
        this.dame = SO.Dame;
        this.firerate = SO.Firerate;
        this.range = SO.Range;
    }
    public abstract void AttackScheme();
    public virtual void OnEquip(Player player) {
        this.transform.parent = player.Hand;
        this.model.transform.DOKill();
        this.transform.localPosition = Vector3.zero;
        this.model.gameObject.SetActive(true);
        this.transform.localRotation = Quaternion.Euler(0,0,0);   
       
    }
    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        if(!player) return;
        player.GetComponent<Player>().Equipment.CollectWeapon(this);    
        player.GetComponent<Player>().Equipment.EquipWeapon();
    }
    public override void OnCollect(Player character)
    {
        base.OnCollect(character);
        this.transform.parent = character.Equipment.transform;
        this.model.transform.localPosition = Vector3.zero;
        this.transform.localPosition = Vector3.zero;
        this.model.gameObject.SetActive(false);
        this.Box.enabled = false;

    }
}
