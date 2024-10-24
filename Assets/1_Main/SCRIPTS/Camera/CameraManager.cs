using System;
using UnityEngine;

public class CameraManager : LazySingleton<CameraManager> 
{
    [Header("Components")]
    [SerializeField] protected CameraMovement movement;
    [SerializeField] protected Camera maincam;
    [Header("variables")]
    public float ScreenWidth;
    public float ScreenLength;
    #region Getter && Setter
    public Camera MainCam {get {return maincam;}}
    public CameraMovement Movement {get {return movement;}}
    #endregion
    protected override void Awake()
    {
        base.Awake();
        if(Instance == null) {
            Debug.Log("is Null");
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMovements();
        this.LoadMainCam();
    }
    protected void LoadMovements() {
        this.movement = GetComponentInChildren<CameraMovement>();
    }
    protected void LoadMainCam() {
        this.maincam = GetComponentInChildren<Camera>();
        this.ScreenLength = maincam.orthographicSize * 2f;
        this.ScreenWidth = maincam.orthographicSize * maincam.aspect * 2;
    }
    protected void FixedUpdate() {
        this.ScreenLength = maincam.orthographicSize * 2f;
        this.ScreenWidth = maincam.orthographicSize * maincam.aspect * 2;
    }
}