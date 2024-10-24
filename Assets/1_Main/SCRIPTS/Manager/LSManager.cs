using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LSManager : MyBehaviour
{
    protected static LSManager instance;
    public static LSManager Instance { get => instance;}
    public static string CharacterDatafilename = "Character.json";
    public static string PropertyDatafilename = "property.json";
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this && instance.gameObject.activeInHierarchy)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Does Existed");
        }
        else  {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        StartCoroutine(this.CrLoadGameData());
    }
    protected IEnumerator CrLoadGameData() {
        yield return new WaitUntil(predicate: () => {
            if(DataManager.Instance == null) return false;
            return true;
        });
        this.LoadGameDatas();
    }
    public void LoadGameDatas() {
        DataManager.Instance.characterDynamicData = LoadGameData<CharacterDynamicData>(CharacterDatafilename);
        DataManager.Instance.propertyDynamicData = LoadGameData<PropertyDynamicData>(PropertyDatafilename);
    }
    public T LoadGameData<T>(string filename) where T: new() {
        string filePath = Path.Combine(Application.persistentDataPath,filename);
        string jsonString = "";

        if (File.Exists(filePath))
        {
            // Read the JSON file from the specified path
            jsonString = File.ReadAllText(filePath);
            Debug.Log("jsonstring: " + jsonString);
        }
        else
        {
            Debug.Log("No save file found.");
            this.SaveGame();
            return new T();
        }
        
        // Parse the JSON string and load the data
        return DataManager.Instance.LoadDataFromJson<T>(jsonString);
    }

    public void LoadGameData(string filename)
    {
        string filePath = Path.Combine(Application.persistentDataPath,filename);
        string jsonString = "";

        if (File.Exists(filePath))
        {
            // Read the JSON file from the specified path
            jsonString = File.ReadAllText(filePath);
            Debug.Log("jsonstring: " + jsonString);
        }
        else
        {
            Debug.Log("No save file found.");
            this.SaveGame();
            return;
        }
        
        // Parse the JSON string and load the data
        DataManager.Instance.LoadCharacterDataFromJson(jsonString);
    }
    public void SaveGame()
    {
        this.SaveGameby(CharacterDatafilename,DataManager.Instance.characterDynamicData);
        this.SaveGameby(PropertyDatafilename,DataManager.Instance.propertyDynamicData);
    }
    protected void SaveGameby(string filename,DynamicData dynamicData) {
        string filePath = Path.Combine(Application.persistentDataPath,filename);  
        // Convert the data to JSON format
        string jsonString = JsonUtility.ToJson(dynamicData);
        
        // Save the JSON string to the specified path
        File.WriteAllText(filePath, jsonString);

        Debug.Log(dynamicData.ToString() + " saved to: " + filePath);
    }

    protected void OnApplicationQuit()
    {
        this.SaveGame();
    }
}
