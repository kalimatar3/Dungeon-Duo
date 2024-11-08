using System.Collections;
using DG.Tweening;
using UnityEngine;
public class GamePlay_Panel : BasePanel
{
    public Transform FloorNotification_Board;
    public Interact_button interact_Button;
    public PlayerAttack_button playerAttack_Button;
    public ChangeWeapon_button changeWeapon_Button;
    public PlayerSkill_button playerSkill_Button;
    private void OnEnable() {
        this.StartCoroutine(this.CrSetActiveChangeWeaponbutton());    
    }
    public void Noticefloor() {
        StartCoroutine(this.CrNoticefloor());
    }
    protected IEnumerator CrNoticefloor() {
        FloorNotification_Board.gameObject.SetActive(true);
        FloorNotification_Board.transform.localScale = new Vector3(1,0);
        FloorNotification_Board.GetComponentInChildren<Message>().message = "Floor " + MapManager.Instance.Curfloor;
        FloorNotification_Board.transform.DOScale(new Vector3(1,1),0.5f);
        yield return new WaitForSeconds(1.5f);
        FloorNotification_Board.transform.DOScale(new Vector3(1,0),0.5f).OnComplete(()=> {
            FloorNotification_Board.gameObject.SetActive(false);
        });
    }
    protected IEnumerator CrSetActiveChangeWeaponbutton() {
        this.changeWeapon_Button.gameObject.SetActive(false);
        yield return new WaitUntil(()=> {
            if(Player.Instance == null) return false;
            if(Player.Instance.Weapon == null) return false;
            return true;
        });
        this.changeWeapon_Button.gameObject.SetActive(true);
    }
}
