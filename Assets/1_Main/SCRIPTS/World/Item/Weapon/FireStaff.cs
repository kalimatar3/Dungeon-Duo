using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStaff : Staff
{
    public override void AttackScheme()
    {
        base.AttackScheme();
        Quaternion quater = Quaternion.LookRotation(this.transform.forward,guntip.transform.up);
        Transform orb = WeaponAttackSpawner.Instance.Spawn("FlameOrb",guntip.transform.position,quater);
        orb.GetComponent<DameDealer>().Dame = this.dame;
    }
}
