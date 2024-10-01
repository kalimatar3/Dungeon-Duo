using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] protected Player character;
    public void InfrontofPlayer() {
        this.transform.position = character.transform.position + character.transform.right * 1f;
    }
    public void FollowTarget(Transform target) {
        this.transform.position = target.transform.position;
    }
}
