using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSetting_button : BaseButton
{
    public override void OnClick()
    {
        PanelManager.Instance.getPanelbyName("Setting_Panel").gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
