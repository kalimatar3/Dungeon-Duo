using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect_Board : MyBehaviour
{
    public Transform CharacterSelectButtonHoler;
    public List<CharacterSelect_button> listcharacterselectbutton;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListCharacterSelectbutton();
        StartCoroutine(this.CrLoadData());
    }
    protected void LoadListCharacterSelectbutton() {
        if(listcharacterselectbutton.Count > 0) return;
        foreach(Transform ele in CharacterSelectButtonHoler) {
            if(ele.GetComponent<CharacterSelect_button>()) listcharacterselectbutton.Add(ele.GetComponent<CharacterSelect_button>());
        }
    }
    protected IEnumerator CrLoadData() {
        yield return new WaitUntil(predicate:()=> {
            if(DataManager.Instance == null) return false;
            if(DataManager.Instance.characterDynamicData == null) return false;
            return true;
        });
        for(int i = 0; i < listcharacterselectbutton.Count;i++) {
            listcharacterselectbutton[i].loadaleCharacterData = DataManager.Instance.characterDynamicData.listcharacterDatas[i];
            listcharacterselectbutton[i].Present();
        }
    }
}
