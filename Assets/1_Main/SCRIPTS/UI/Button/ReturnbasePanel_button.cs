using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnbasePanel_button : BaseButton
{
    public override void OnClick()
    {
        PanelManager.Instance.ReturntoMainMenu();
    }
}
