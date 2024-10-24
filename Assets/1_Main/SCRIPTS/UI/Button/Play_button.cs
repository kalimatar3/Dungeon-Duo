using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_button : BaseButton
{
    public Character_Panel Character_Panel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterPanel();
    }
    protected void LoadCharacterPanel() {
        this.Character_Panel = GetComponentInParent<Character_Panel>();
    }
    public override void OnClick()
    {
        DataManager.Instance.CurCharacterData = Character_Panel.loadedCharacterData;
        ScenesManager.Instance.LoadScenebyName(MyScene.GamePlayScene.ToString());
    }
}
