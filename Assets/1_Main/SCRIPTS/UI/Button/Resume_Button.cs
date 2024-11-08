using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume_Button : BaseButton
{
    public override void OnClick()
    {
        Time.timeScale = 1f;
        PanelManager.Instance.getPanelbyName("Setting_Panel").gameObject.SetActive(false);
    }
}
