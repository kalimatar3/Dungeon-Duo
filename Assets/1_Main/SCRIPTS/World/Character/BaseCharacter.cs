using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MyBehaviour
{
    [Header("Components")]
    [SerializeField] protected Transform hand;
    [SerializeField] protected DameReciver reciver;
    #region getter && setter
    public Transform Hand {get {return hand;}}
    public DameReciver Reciver { get {return reciver;}}
    #endregion
    #region Loadcomponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadhand();
        this.LoadReciver();
    }
    protected void Loadhand() {
        this.hand = this.transform.Find("Hand");
    }
    protected void LoadReciver() {
        this.reciver = GetComponentInChildren<DameReciver>();
    }
    #endregion 
}
