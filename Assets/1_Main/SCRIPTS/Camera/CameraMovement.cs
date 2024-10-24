using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MyBehaviour
{
    [SerializeField] protected float smooth;
    protected void FixedUpdate() {
        if(Player.Instance) this.FollowCharacter(Player.Instance);
    }
    protected void FollowCharacter(Player player) {
        Vector3 nextPos = new Vector3(player.transform.position.x,player.transform.position.y,this.transform.position.z);
        this.transform.parent.position = Vector3.Lerp(this.transform.parent.position,nextPos, Time.deltaTime *1f * smooth);
    }
}
