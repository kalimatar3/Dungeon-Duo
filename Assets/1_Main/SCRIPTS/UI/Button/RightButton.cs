using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : MyBehaviour
{
    [Header("DragComponents")]
    [Header("LoadComponent")]
    [SerializeField] protected PlayerAttack_button playerAttack_Button;
    [SerializeField] protected Interact_button interact_Button;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadinteract();
        this.LoadPlayerattack();
    }
    protected void LoadPlayerattack() {
        this.playerAttack_Button = this.GetComponentInChildren<PlayerAttack_button>();
    }
    protected void Loadinteract() {
        this.interact_Button = this.GetComponentInChildren<Interact_button>();
        this.interact_Button.player = Player.Instance;
    }
    protected void FixedUpdate() {
        if(Player.Instance.Collector.IScheck) {
            this.interact_Button.gameObject.SetActive(true);
            this.playerAttack_Button.gameObject.SetActive(false);
        }
        else {
            this.interact_Button.gameObject.SetActive(false);
            this.playerAttack_Button.gameObject.SetActive(true);
        }
    }
}
