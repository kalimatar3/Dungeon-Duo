using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Character : Player
{
    public static float SkillDame = 2;
    protected override void CalculateStats(int level)
    {
        this.MaxHp = characterSO.BaseStatistic.Hp;
        this.MaxDp = characterSO.BaseStatistic.Dp;
        this.skillCooldownTime = characterSO.BaseStatistic.SkillCooldownTime;
        this.SkillMpCost = characterSO.BaseStatistic.SkillMpCost;
        this.MaxMp = characterSO.BaseStatistic.Mp;
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
