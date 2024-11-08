using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_button : BaseButton
{
    public override void OnClick()
    {
        ScenesManager.Instance.LoadScenebyName(MyScene.MainMenuScene.ToString());
        Time.timeScale = 1f;
    }
}
