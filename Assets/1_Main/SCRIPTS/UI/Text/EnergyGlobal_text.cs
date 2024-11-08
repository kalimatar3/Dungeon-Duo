using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGlobal_text : BaseTextUI
{
    public override void ShowText()
    {
        text.text = DataManager.Instance.propertyDynamicData.Energy.ToString("F0");
    }
}
