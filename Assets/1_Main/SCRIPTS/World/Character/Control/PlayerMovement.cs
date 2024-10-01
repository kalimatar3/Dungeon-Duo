using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MyBehaviour
{
    [SerializeField] protected Player character;
    [SerializeField] protected bool isfacing;
    [SerializeField] protected float Speed = 5f;
    public bool IsFacing {get {return isfacing;} set {isfacing = value;}}
    public void Move() {
        if(!isfacing && InputManager.Instance.MovingInput.x != 0) this.character.transform.right = new Vector3(InputManager.Instance.MovingInput.x,0).normalized;
        this.character.transform.position += new Vector3(InputManager.Instance.MovingInput.x,InputManager.Instance.MovingInput.y) * Speed * Time.deltaTime * 1f;
    }
    public void MovebyKeyboard() {
    }
    public void FacingtoTarget(Transform target) {
        if(isfacing) this.character.transform.right = (new Vector3(target.position.x,0,0) - new Vector3(this.character.transform.position.x,0,0)).normalized;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadcharacter();
    }
    protected void Loadcharacter() {
        this.character = this.GetComponentInParent<Player>();
    }
    protected void FixedUpdate() {
        this.Move();
    }

}
