using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] protected Player player;
    public void InfrontofPlayer() {
        if(InputManager.Instance.MovingInput != Vector2.zero) {
            this.transform.position = player.transform.position +  (Vector3)InputManager.Instance.MovingInput; 
        }
        else this.transform.position = player.transform.position + player.transform.right * 1f;
    }
    public void FollowTarget(Transform target) {
        this.transform.position = target.transform.position;
    }
}
