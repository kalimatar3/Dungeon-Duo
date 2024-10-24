using UnityEngine;
using UnityEngine.UI;

public class Player_Board : MyBehaviour
{
    [SerializeField] protected GlobalDp_Slider globalDp_Slider;
    [SerializeField] protected GlobalHp_Slider globalHp_Slider;
    [SerializeField] protected GlobalMp_Slider globalMp_Slider;
    public Image image;
    public void Refresh() {
        this.globalDp_Slider.SetvalueSlider(Player.Instance.Reciver.Dp/Player.Instance.MaxDp);
        this.globalHp_Slider.SetvalueSlider(Player.Instance.Reciver.Hp/Player.Instance.MaxHp);
        this.globalMp_Slider.SetvalueSlider(Player.Instance.CurMp/Player.Instance.MaxMp);
    }
}
