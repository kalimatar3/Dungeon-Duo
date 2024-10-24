using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelect_button : BaseButton
{
    [SerializeField] protected Image image;
    [SerializeField] protected Message Name,Level;
    public LoadaleCharacterData loadaleCharacterData;
    public override void OnClick()
    {
        Character_Panel character_Panel = PanelManager.Instance.getPanelbyName("Character_Panel").GetComponent<Character_Panel>();
        character_Panel.loadedCharacterData = this.loadaleCharacterData;
        character_Panel.transform.gameObject.SetActive(true);
    }
    public void Present() {
        Player character = DataManager.Instance.GetPlayerbyName(loadaleCharacterData.Name);
        this.image.sprite = character.SO.Icon;
        this.Name.message = loadaleCharacterData.Name;
        this.Level.message = "Level " + (loadaleCharacterData.level + 1).ToString("F0");
    }
} 
