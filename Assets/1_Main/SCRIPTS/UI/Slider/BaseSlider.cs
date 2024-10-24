using UnityEngine;
using UnityEngine.UI;
public abstract class BaseSlider : MyBehaviour
{
    [SerializeField] protected Slider slider;
    protected void LoadSlider()
    {
        Slider slider =  GetComponent<Slider>();
        if(slider == null)
        {
            Debug.LogWarning(this.transform.name + " Cant Found Slider");
            return;
        }
        this.slider = slider;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSlider();
    }
    public virtual void SetvalueSlider(float number)
    {
        this.slider.value = number;
    }
    public virtual float getvalueSlider()
    {
        return this.slider.value;
    }
}
