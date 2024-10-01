using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_button : BaseButton
{
    public Player player;
    public override void Act()
    {
        StartCoroutine(this.CrAct());
    }
    protected IEnumerator CrAct() {
        player.Attack();
        this.Permission = false;
        yield return new WaitForSeconds(player.Weapon.Firerate);
        this.Permission = true;
    }
}
