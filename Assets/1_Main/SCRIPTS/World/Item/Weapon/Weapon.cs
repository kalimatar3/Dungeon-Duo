using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : BaseItem
{
    [Header("Components")]
    //
    [Header("Statisitcs")]
    [SerializeField] protected float dame;
    [SerializeField] protected float firerate;
    [SerializeField] protected float range;
    #region Getter & Setter
    public float Dame {get {return dame;}}
    public float Firerate {get {return firerate;}}
    public float Range {get {return range;} }
    #endregion
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.InitStatistics();
        this.InitData();
    }
    /// <summary>
    ///  Dame
    ///  firerate
    ///  Range
    /// </summary>
    protected abstract void InitStatistics();
    /// <summary>
    /// Sprite
    /// </summary>
    protected abstract void InitData();
    public abstract void Attack();
    public override void OnCollect(BaseCharacter character)
    {
        base.OnCollect(character);
        if(!(Player)character) return;
        character.GetComponent<Player>().Equipment.EquipWeapon(this);
      this.model.gameObject.SetActive(true);
    }
}
