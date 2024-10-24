using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Character_Panel : BasePanel
{
    [Header("Data")]
    public LoadaleCharacterData loadedCharacterData;
    [SerializeField] protected Player player;
    [Header("Components")]
    public Message CharacterDescription;
    public Message SkillDescription;
    public Image Icon;
    public Image SKillIcon;
    public Message PanelName_Text;
    public Message Level_Text;
    protected void OnEnable() {
        this.StartCoroutine(this.CrLoadPlayer());
    }
    protected IEnumerator CrLoadPlayer() {
        yield return new WaitUntil(predicate:()=> {
            if(loadedCharacterData == null) return false;
            return true;
        });
        player = DataManager.Instance.GetPlayerbyName(loadedCharacterData.Name);
        this.Present();
    }
    public void Refresh() {
        // refreshx
        this.Present();
    }
    public void Present() {
        this.PanelName_Text.message = player.SO.name;
        this.CharacterDescription.message = player.SO.CharacterDescription;
        this.SkillDescription.message = player.SO.SkillDescription;
        this.Icon.sprite = player.SO.Icon;
        this.SKillIcon.sprite = player.SO.SkillIcon;
        this.Level_Text.message = "level " + loadedCharacterData.level.ToString();
    }
}
