using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelupCharacter_button : BaseButton
{
    public Character_Panel character_Panel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterPanel();
    }
    protected void LoadCharacterPanel() {
        this.character_Panel = GetComponentInParent<Character_Panel>();
    }
    public override void OnClick()
    {
        this.character_Panel.loadedCharacterData.level ++;
        character_Panel.Refresh();
    }
}
