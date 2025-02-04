public class PlayerLane : Lane
{
    public override void InitializeHorse(Horse horse)
    {
        horse.IsPlayerHorse = true;
    }
}