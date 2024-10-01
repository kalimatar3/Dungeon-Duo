using UnityEngine;

public class LazySingleton<T> : MyBehaviour where T : LazySingleton<T> {
    [SerializeField] protected static T instance;
    public static T Instance {get {return instance;}}
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != (T)this) {
            Destroy((T)this);
        }
        else instance = (T)this;
    }
}