using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float Wandaspeed;
    public float TrackingSpeed;
    public float MaxHp;
    public float DetectionRange;
    public float AttackRange;
    public float FireRate;
}
