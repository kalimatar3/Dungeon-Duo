public class PlayerState : State
{
    protected Player character;
    public PlayerState(Player player, Statemachine statemachine) : base(statemachine)
    {
        this.character = player;
    }
}
