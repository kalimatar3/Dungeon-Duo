using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MyBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance {get => instance;}
    [SerializeField] protected VariableJoystick leftJoyStick;
    public VariableJoystick LeftjoyStick {get {return leftJoyStick;}}
    public Vector2 MovingInput; 
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this) {
            Destroy(this);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    protected void FixedUpdate() {
        this.GetMovingInput();
    }
    protected void GetMovingInput() {
        this.MovingInput = new Vector2(leftJoyStick.Horizontal,leftJoyStick.Vertical);
        this.MovingInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }
}
