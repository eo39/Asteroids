namespace Asteroids;

internal class CommandChangeShotgunDelay : ICommand
{
    private readonly IArmedObject armedObject;
    private readonly int delta;
    private readonly int oldDelay;

    public CommandChangeShotgunDelay(IArmedObject armedObject, int delta)
    {
        this.armedObject = armedObject;
        this.delta       = delta;

        this.oldDelay = this.armedObject.DelayOfShot;
    }

    public void Execute()
    {
        int newDelay = this.oldDelay + this.delta;

        this.armedObject.DelayOfShot = newDelay switch
        {
            <= 0  => 0,
            >= 15 => 15,
            _     => newDelay
        };
    }

    public void Undo()
    {
        this.armedObject.DelayOfShot = this.oldDelay;
    }
}