namespace Asteroids;

internal class CommandChangeSpeed : ICommand
{
    private readonly GameObject gameObject;
    private readonly double delta;
    private readonly double oldSpeed;

    public CommandChangeSpeed(GameObject gameObject, double delta)
    {
        this.gameObject = gameObject;
        this.delta      = delta;

        this.oldSpeed = this.gameObject.Speed;
    }

    public void Execute()
    {
        double newSpeed = this.oldSpeed + this.delta;

        this.gameObject.Speed = newSpeed switch
        {
            <= 0  => 0,
            >= 10 => 10,
            _     => newSpeed
        };
    }

    public void Undo()
    {
        this.gameObject.Speed = this.oldSpeed;
    }
}