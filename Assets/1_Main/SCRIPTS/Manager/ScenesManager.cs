using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public struct MyScene 
{
    public static string LoadingScene = "0_LoadingScence";
    public static string MainMenuScene = "1_MainMenuScene";
    public static string GamePlayScene = "2_GamePlayScene";
}
public class ScenesManager : LazySingleton<ScenesManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        this.LoadScenebyName(MyScene.MainMenuScene.ToString());
    }
    public void LoadScenebyName(string scenename)
    {
        var scene = SceneManager.LoadSceneAsync(scenename);
    }
    protected IEnumerator Loading(string scene)
    {
        float value = 0;
        var Scene = SceneManager.LoadSceneAsync(scene);
        Scene.allowSceneActivation = false;
        do
        {
            value += Time.deltaTime * 1f;
            yield return new WaitForSeconds(Time.deltaTime * 1f);
        } while (!this.Canload());
        yield return new WaitForSeconds(0.2f);
        Scene.allowSceneActivation = true;
        Debug.Log("Load " + "MenuScene" + " Done");
    }
    public void ReloadCurScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    protected bool Canload()
    {
        if (DataManager.Instance == null) return false;
        return true;
    }
}
