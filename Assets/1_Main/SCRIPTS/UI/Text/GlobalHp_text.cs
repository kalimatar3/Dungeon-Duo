    public class GlobalHp_text : BaseTextUI
{
    public override void ShowText()
    {
        if(Player.Instance == null) return;
        text.text = Player.Instance.Reciver.Hp.ToString("F0") + "/" + Player.Instance.MaxHp.ToString("F0");
    }
}
