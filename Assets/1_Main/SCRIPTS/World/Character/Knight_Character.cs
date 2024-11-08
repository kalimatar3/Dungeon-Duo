using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Character : Player
{
    [SerializeField] protected Shield shield;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadShield();
    }
    private void loadShield() {
        this.shield = GetComponentInChildren<Shield>();
        this.shield.gameObject.SetActive(false);
    }
    protected override void SkillScheme()
    {
        this.shield.Lifetime = 5f;
        this.shield.gameObject.SetActive(true);
    }
    protected override void CalculateStats(int level)
    {
        this.MaxHp = playerSO.BaseStatistic.Hp;
        this.MaxDp = playerSO.BaseStatistic.Dp;
        this.skillCooldownTime = playerSO.BaseStatistic.SkillCooldownTime;
        this.SkillMpCost = playerSO.BaseStatistic.SkillMpCost;
        this.MaxMp = playerSO.BaseStatistic.Mp;
    }
}
