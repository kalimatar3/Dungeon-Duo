using System.Collections.Generic;
using System.Net;
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
    public void EquipWeapon(Weapon weapon) {
        if(weapons.Count > 1 && weapon == weapons.Peek()) return;
        if(weapons.Count > 2) weapons.Pop();
        weapons.Push(weapon);
        weapon.transform.parent = this.transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.Model.transform.localPosition = Vector3.zero;
        weapon.Model.gameObject.SetActive(false);
        weapon.Box.enabled = false;
    }
    public void ChangeWeapon() {
        if(weapons.Count !=2) return;
        Weapon weapon1 = weapons.Pop();
        Weapon weapon2 = weapons.Pop();
        weapons.Push(weapon2);
        weapons.Push(weapon1);
        character.Weapon = weapons.Peek();
    }
}
