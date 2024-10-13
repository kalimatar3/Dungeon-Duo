using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string Name;
    public float Dame;
    public float Firerate;
    public float Range;
}
