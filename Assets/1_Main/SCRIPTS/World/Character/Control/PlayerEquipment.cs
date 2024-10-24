using System.Collections.Generic;
using System.Net;
using DG.Tweening;
using UnityEngine;
public class PlayerEquipment : MyBehaviour
{
    [SerializeField] protected Player character;
    [SerializeField] protected Stack<Weapon> weapons = new Stack<Weapon>();
    public Stack<Weapon> Weapons {get {return weapons;}}
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadcharacter();
        weapons = new Stack<Weapon>();
    }
    protected void Loadcharacter() {
        this.character = GetComponentInParent<Player>();
    }
    public void CollectWeapon(Weapon weapon) {
        if(weapons.Count >= 1) 
        {
            Weapon weapon1 = weapons.Peek();
            weapon1.OnCollect(character);
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
        character.Weapon = weapon;
        weapon.OnEquip(character);
    }
    public void ChangeWeapon() {
        if(weapons.Count !=2) return;
        Weapon weapon1 = weapons.Pop();
        Weapon weapon2 = weapons.Pop();
        weapons.Push(weapon2);
        weapons.Push(weapon1);
        this.EquipWeapon();
    }
}
