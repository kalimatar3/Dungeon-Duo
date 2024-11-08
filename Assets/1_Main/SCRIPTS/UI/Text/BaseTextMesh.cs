using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTextMesh : MyBehaviour
{
    [SerializeField] protected TextMesh textMesh;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.textMesh = GetComponent<TextMesh>();
    }   
}
