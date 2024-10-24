using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public abstract class BaseButton : MyBehaviour,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected Button thisbutton;
    [SerializeField] protected bool Permission = true;
    protected bool Doing;
    public void setPermission(bool trigger)
    {
        this.Permission = trigger;
    }
    protected virtual void OnEnable()
    {
        this.Permission = true;
        this.Doing = false;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadthisbutton();
        this.AddActButton();
    }
    protected void Loadthisbutton()
    {
        thisbutton = GetComponent<Button>();
        if(thisbutton == null) Debug.LogWarning( this.transform + "dont have button");
    }
    protected void AddActButton()
    {
        if(thisbutton == null) return;
        thisbutton.onClick.AddListener(delegate () { this.DelayAct(); });
    }
    protected void DelayAct()
    {
        if (!CanAct()) return;
        this.OnClick();
    }
    protected virtual bool CanAct()
    {
        return Permission;
    }
    public void setcanAct(bool canAct)
    {
        this.Permission = canAct;
    }
    public abstract void OnClick();

    public void OnPointerDown(PointerEventData eventData)
    {
        this.Doing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.Doing = false;
    }
}
