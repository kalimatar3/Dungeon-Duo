using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReciver : DameReciver
{
    public override void Dead()
    {
        //base.Dead();
    }
    public override void DeductHp(float dame)
    {
    }
    protected override void LoadBox()
    {
        base.LoadBox();
        this.Box.isTrigger = false;
    }
}
