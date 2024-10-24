using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayLoading_panel : BasePanel
{
    private void Start() {
        StartCoroutine(this.CrLoading()); 
    }
    protected IEnumerator CrLoading() {
        yield return new WaitUntil(predicate:() => {
            if(!MapManager.Instance.IsCompletedLoadedMap) return false;
            if(!DataManager.Instance.IsCompletedLoadeddata) return false;
            return true;
        });
        this.gameObject.SetActive(false);
        PanelManager.Instance.ReturntoMainMenu();
    }
}
