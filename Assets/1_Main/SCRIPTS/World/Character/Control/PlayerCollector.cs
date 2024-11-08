using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class PlayerCollector : MyBehaviour
{
    [SerializeField] protected bool isCheck;
    [SerializeField] protected Player player;
    [SerializeField] protected CircleCollider2D box;
    [SerializeField] protected Iinteractable listInteractable;
    public Iinteractable ListInteractable {get {return listInteractable;}}
    public bool IScheck {get {return isCheck;}}
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadcollider();
        this.Loadcharacter();
    }
    protected void Loadcharacter() {
        this.player = GetComponentInParent<Player>();
    }
    protected void Loadcollider() {
        this.box = this.GetComponent<CircleCollider2D>();
        this.box.isTrigger = true;
    }
    protected void FixedUpdate() {
        if(ListInteractable != null) 
        {
            isCheck = true;
        }
        else isCheck = false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.GetComponent<Gold>()) {
            other.GetComponent<Gold>().OnInteract(player);
            return;
        }
        if(other.GetComponent<Energyball>()) {
            other.GetComponent<Energyball>().OnInteract(player);
            return;
        }
        if(other.GetComponentInParent<Iinteractable>() != null)
        this.listInteractable = other.GetComponentInParent<Iinteractable>();
        else if(other.GetComponent<Iinteractable>() != null)
        this.listInteractable = other.GetComponent<Iinteractable>();
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<Iinteractable>() != null || other.GetComponentInParent<Iinteractable>() != null)
        {
            this.listInteractable = null;
        }
    }
 }
