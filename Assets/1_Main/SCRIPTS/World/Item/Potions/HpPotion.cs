using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : Potion
{
    [SerializeField] protected float amountofHP;
    [SerializeField] protected float Radius;
    public float AmountofHP {get {return amountofHP;} set {amountofHP = value;}}
    public override void OnInteract(Player player)
    {
        player.Reciver.IcrHP(amountofHP);    
        this.DeSpawn();   
    }

}
