using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Message : BaseTextUI
{
    [SerializeField] protected string insidemessage;
    public string message { get {return insidemessage;} 
        set { 
                insidemessage = value;
                this.text.text = value;
            }
        }
    public override void ShowText()
    {
        this.text.text = insidemessage;
    }
}
