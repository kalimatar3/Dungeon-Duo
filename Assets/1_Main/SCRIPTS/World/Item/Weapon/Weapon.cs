using System.Collections;
using System.Collections.Generic;
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
    public override void OnCollect(BaseCharacter character)
    {
        base.OnCollect(character);
        if(!(Player)character) return;
        character.GetComponent<Player>().Equipment.EquipWeapon(this);
        character.GetComponent<Player>().Weapon = character.GetComponent<Player>().Equipment.Weapons.Peek();
    }
}
