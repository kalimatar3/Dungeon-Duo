using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Character : Player
{
    public static float SkillDame = 2;
    protected override void CalculateStats(int level)
    {
        this.MaxHp = playerSO.BaseStatistic.Hp;
        this.MaxDp = playerSO.BaseStatistic.Dp;
        this.skillCooldownTime = playerSO.BaseStatistic.SkillCooldownTime;
        this.SkillMpCost = playerSO.BaseStatistic.SkillMpCost;
        this.MaxMp = playerSO.BaseStatistic.Mp;
    }
    protected override void SkillScheme()
    {
        for(int i = 0 ; i < 5; i++)
        {
            Quaternion quaternion = Quaternion.Euler(0,0,72 * i);
            Transform orb =  WeaponAttackSpawner.Instance.Spawn("FlameOrb",this.transform.position,quaternion);
            orb.GetComponent<DameDealer>().Dame = 2f;
        }
    }
}
