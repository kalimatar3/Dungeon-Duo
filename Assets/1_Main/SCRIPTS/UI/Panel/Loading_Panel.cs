using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_Panel : BasePanel
{
    protected void Load() {
        ScenesManager.Instance.LoadScenebyName(MyScene.MainMenuScene.ToString());
    }
    private void OnEnable() {
        this.Load();
    }
}
