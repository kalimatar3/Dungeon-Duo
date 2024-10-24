using UnityEngine;
public abstract class Buy_button : BaseButton
{
    public override void OnClick()
    {
        if(Condition()) 
        {
            this.Pay();
            this.GetMechandise();
        }
        else Debug.Log(this.transform.name + "Cant Buy");
    }
    public abstract void GetMechandise();
    protected abstract void Pay();
    public abstract bool Condition();
}
