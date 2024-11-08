using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character_Panel : BasePanel
{
    [Header("Data")]
    public LoadableCharacterData loadedCharacterData;
    public Player player;
    [Header("Components")]
    public LevelupCharacter_button levelupCharacter_Button;
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
        this.Level_Text.message = "level " + (loadedCharacterData.level +1).ToString();
        this.levelupCharacter_Button.Price_text.message = player.GetLevelupPricebyLevel(loadedCharacterData.level).ToString();
        this.levelupCharacter_Button.Price = player.GetLevelupPricebyLevel(loadedCharacterData.level);
    }
}
