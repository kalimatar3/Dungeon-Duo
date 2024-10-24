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
        this.MaxHp = characterSO.BaseStatistic.Hp;
        this.MaxDp = characterSO.BaseStatistic.Dp;
        this.skillCooldownTime = characterSO.BaseStatistic.SkillCooldownTime;
        this.SkillMpCost = characterSO.BaseStatistic.SkillMpCost;
        this.MaxMp = characterSO.BaseStatistic.Mp;
    }
}
