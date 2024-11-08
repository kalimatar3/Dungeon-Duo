using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelupCharacter_button : Buy_button
{
    public Character_Panel character_Panel;
    public float Price;
    public Message Price_text;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterPanel();
    }
    protected void LoadCharacterPanel() {
        this.character_Panel = GetComponentInParent<Character_Panel>();
    }
    public override void GetMechandise()
    {
        this.character_Panel.loadedCharacterData.level ++;
        character_Panel.Refresh();
    }

    protected override void Pay()
    {
        DataManager.Instance.propertyDynamicData.Energy -= Price;
    }

    public override bool Condition()
    {
        return DataManager.Instance.propertyDynamicData.Energy >= Price;
    }
}
