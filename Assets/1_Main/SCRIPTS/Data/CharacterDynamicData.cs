using System.Collections.Generic;
[System.Serializable]
public class CharacterDynamicData : DynamicData
{
    public List<LoadableCharacterData> listcharacterDatas;
}
[System.Serializable]
public class LoadableCharacterData {
     public LoadableCharacterData(string name) {
        this.Name = name;
        this.level = 0;
    }
    public string Name;
    public int level;
}
