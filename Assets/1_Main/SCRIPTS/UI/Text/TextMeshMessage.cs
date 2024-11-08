using System.Runtime.CompilerServices;
using UnityEngine;

public class TextMeshMessage : BaseTextMesh 
{
    [SerializeField] protected string message;
    public string  Message {get {return message;} 
                            set
                            {
                                this.message = value;
                                this.textMesh.text = value;
                            }}
}