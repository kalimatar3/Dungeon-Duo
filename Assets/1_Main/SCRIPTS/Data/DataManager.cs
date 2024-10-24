using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
[DefaultExecutionOrder(-100)]
public class DataManager : LazySingleton<DataManager>
{
    [SerializeField] internal bool isCompletedLoadingData;
    public Player[] Characters;
    [Header("DynamicData")]
    public CharacterDynamicData characterDynamicData; 
    public PropertyDynamicData  propertyDynamicData;
    public LoadaleCharacterData CurCharacterData;
    public bool IsCompletedLoadeddata;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void LoadCharacterDataFromJson(string jsonString) {
        CharacterDynamicData characterdata = JsonUtility.FromJson<CharacterDynamicData>(jsonString);
        if(characterdata == null) {
            Debug.Log("characterdynamicdata is null");
        }
        else this.characterDynamicData = characterdata;
    }
    public T LoadDataFromJson<T>(string jsonString) where T : new() {
        T DynamicData = JsonUtility.FromJson<T>(jsonString);
        if(DynamicData == null) 
        {
            Debug.Log(typeof(T) + " is null");
            DynamicData =  new T();
        }
        return DynamicData;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterFromResources();
        StartCoroutine(this.CrInitPlayerDynamicData());
        StartCoroutine(this.CrCheckLoadedData());
    }
    protected IEnumerator CrCheckLoadedData() {
        this.isCompletedLoadingData = false;
        yield return new WaitUntil(predicate:()=> {
            if(this.propertyDynamicData == null) return false;
            if(this.characterDynamicData ==null) return false;
            return true;
        });
        this.IsCompletedLoadeddata = true;
    }
    protected IEnumerator CrInitPlayerDynamicData() {
        yield return new WaitUntil(predicate:()=>{
            if(Characters == null) return false;
            return true;
        });
        this.InitPlayerDynamicData();
    }
    protected void InitPlayerDynamicData() {
        if(characterDynamicData.listcharacterDatas.Count > 0) return;
        foreach(var ele in Characters) {
            LoadaleCharacterData characterData = new LoadaleCharacterData(ele.name);
            characterDynamicData.listcharacterDatas.Add(characterData);
        }
    }
    protected void LoadCharacterFromResources() {
        string path = "Characters/";
        Player[] characters = Resources.LoadAll<Player>(path);
        this.Characters = characters;
    }
    public Player GetPlayerbyName(string name) {
        foreach(var ele in Characters) {
            if(ele.name == name) return ele;
        }
        return null;
    }
}
