using System.Collections.Generic;
using System.Net;
using DG.Tweening;
using UnityEngine;
public class PlayerEquipment : MyBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected Stack<Weapon> weapons = new Stack<Weapon>();
    public Stack<Weapon> Weapons {get {return weapons;}}
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadcharacter();
        weapons = new Stack<Weapon>();
    }
    protected void Loadcharacter() {
        this.player = GetComponentInParent<Player>();
    }
    public void CollectWeapon(Weapon weapon) {
        if(weapons.Count >= 1) 
        {
            Weapon weapon1 = weapons.Peek();
            weapon1.OnCollect(player);
        }
        if(weapons.Count >= 2) 
        {
            Weapon thisweapon = weapons.Pop();
            thisweapon.transform.position = weapon.transform.position;
            thisweapon.OnDrop();
        }
        weapons.Push(weapon);
    }
    public void EquipWeapon() {
        Weapon weapon = weapons.Peek(); 
        player.Weapon = weapon;
        player.UI.changeWeapon_Button.Icon.sprite = weapon.Data.Icon;
        player.UI.changeWeapon_Button.message.message = "Level " +  (weapon.level + 1).ToString("F0");
        weapon.OnEquip(player);
        
    }
    public void ChangeWeapon() {
        if(weapons.Count !=2) return;
        Debug.Log("ChangeWeapon");
        Weapon weapon1 = weapons.Pop();
        Weapon weapon2 = weapons.Pop();
        weapons.Push(weapon1);
        weapons.Push(weapon2);
        weapon1.OnCollect(player);
        this.EquipWeapon();
    }
}
