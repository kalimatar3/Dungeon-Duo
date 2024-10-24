using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterDynamicData : DynamicData
{
    public List<LoadaleCharacterData> listcharacterDatas;
}
[System.Serializable]
public class LoadaleCharacterData {
     public LoadaleCharacterData(string name) {
        this.Name = name;
        this.level = 0;
    }
    public string Name;
    public int level;
}
