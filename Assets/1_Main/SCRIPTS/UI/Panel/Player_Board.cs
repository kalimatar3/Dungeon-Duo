using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Board : MyBehaviour
{
    [SerializeField] protected GlobalDp_Slider globalDp_Slider;
    [SerializeField] protected GlobalHp_Slider globalHp_Slider;
    [SerializeField] protected GlobalMp_Slider globalMp_Slider;
    public Image image;
    protected void OnEnable() {
        StartCoroutine(this.CrDelayLoadplayer());
    }
    protected IEnumerator CrDelayLoadplayer() {
        yield return new WaitUntil(()=> {
            if(Player.Instance == null) return false;
            return true;
        });
        this.image.sprite = Player.Instance.SO.Icon;
        this.globalDp_Slider.SetvalueSlider(Player.Instance.Reciver.Dp/Player.Instance.MaxDp);
        this.globalHp_Slider.SetvalueSlider(Player.Instance.Reciver.Hp/Player.Instance.MaxHp);
        this.globalMp_Slider.SetvalueSlider(Player.Instance.CurMp/Player.Instance.MaxMp);
    }
}
