using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StraightMovement : MyBehaviour
{
    [SerializeField] protected float speed = 1;
    private void FixedUpdate() {
        this.transform.parent.position += this.transform.up * speed * Time.deltaTime;    
    }
}
