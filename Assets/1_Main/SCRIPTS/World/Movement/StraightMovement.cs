using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StraightMovement : BaseMovement
{
    public override void MoveScheme()
    {
        this.transform.parent.position += this.transform.up * speed * Time.deltaTime;    
    }
    private void FixedUpdate() {
        this.MoveScheme();
    }
}
