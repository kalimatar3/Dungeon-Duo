using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : BaseItem
{
    [SerializeField] protected float amountofHP;
    public float AmountofHP {get {return amountofHP;} set {amountofHP = value;}}
    public override void OnCollect(BaseCharacter character)
    {
        base.OnCollect(character);
        character.Reciver.IcrHP(amountofHP);
    }
}
