using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MyBehaviour
{
    [SerializeField] protected BaseCharacter character;
    [SerializeField] protected float smooth;
    protected void FixedUpdate() {
        this.FollowCharacter(character);
    }
    protected void FollowCharacter(BaseCharacter character) {
        Vector3 nextPos = new Vector3(character.transform.position.x,character.transform.position.y,this.transform.position.z);
        this.transform.parent.position = Vector3.Lerp(this.transform.parent.position,nextPos, Time.deltaTime *1f * smooth);
    }
}
