using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UpgradeShop : MyBehaviour, Iinteractable
{
    [SerializeField] protected Collider2D box;
    [SerializeField] protected TextMeshMessage message;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.box= GetComponent<Collider2D>();
        this.box.isTrigger = true;
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
    public void OnInteract(Player player)
    {
        if(GoodbyeDialogCr != null) StopCoroutine(GoodbyeDialogCr);
        if(MapManager.Instance.Goldnumber < player.Weapon.GetUpgradePricebylevel()) {
            this.message.Message = "You don't have enough golds";
            return;
        }
        player.Weapon.Upgrade();
        message.Message = "Want to upgrade " + player.Weapon.name + " With " + "<color=F1B800>" + player.Weapon.GetUpgradePricebylevel() + "<color/>" + " golds ?";
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.GetComponent<Player>()) {
            if(GoodbyeDialogCr != null) StopCoroutine(GoodbyeDialogCr);
            if(other.transform.GetComponent<Player>().Weapon == null) {
                message.Message = "Enter this place with no weapon ? Are you sure.";
            }
            else {
                Weapon weapon = other.transform.GetComponent<Player>().Weapon;
                message.Message  = "Want to upgrade " + weapon.name + " With " + "<color=#F1B800>" + weapon.GetUpgradePricebylevel() + "</color>" + " golds ?";
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.GetComponent<Player>()) {
            if(this.transform.gameObject.activeInHierarchy) this.GoodbyeDialogCr =  StartCoroutine(this.CrGoodbyeDialog());
        }    
    } 
    protected Coroutine GoodbyeDialogCr; 
    protected IEnumerator CrGoodbyeDialog() {
        message.Message = "Oh. Not interesting!";
        yield return new WaitForSeconds(1f);
        message.Message = "See you around...";
        yield return new WaitForSeconds(1f);
        message.Message = "";
    }
}