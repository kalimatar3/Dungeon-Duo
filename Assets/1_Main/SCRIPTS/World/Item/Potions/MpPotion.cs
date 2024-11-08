using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MpPotion : Potion
{
    [SerializeField] protected float amountofHP;
    [SerializeField] protected float Radius;
    public float AmountofHP {get {return amountofHP;} set {amountofHP = value;}}
    public override void OnInteract(Player player)
    {
        player.IcrMp(amountofHP);    
        this.DeSpawn();   
    }

}
