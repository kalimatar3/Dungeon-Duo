using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GamePlay_Panel : BasePanel
{
    public Transform FloorNotification_Board;
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
}
