using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Schema;
using UnityEngine;

public class PlayerEquipment : MyBehaviour
{
    [SerializeField] protected Player character;
    [SerializeField] protected Weapon[] ListWeapon = new Weapon[2];
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadcharacter();
        this.ListWeapon = new Weapon[2];
    }
    protected void Loadcharacter() {
        this.character = GetComponentInParent<Player>();
    }
    public void EquipWeapon(Weapon weapon) {
        for(int i = 0 ; i < ListWeapon.Length; i++) {
            if(ListWeapon[i] == null) {
                ListWeapon[i] = weapon;
                this.character.Weapon = ListWeapon[i];
                return;
            }
        }
        for(int i = 0 ; i < ListWeapon.Length; i++) { 
            if(character.Weapon == ListWeapon[i]) {
                ListWeapon[i] = weapon;
                return;
            }
        }
    }
    public void ChangeWeapon(Weapon weapon) {
        foreach(var ele in ListWeapon) {
            if(ele == weapon) {
                character.Weapon = weapon;
            }
        }
        Debug.LogWarning("Cant Found" + weapon.ItemName + "in ListWeapon");
        return;
    }
}
