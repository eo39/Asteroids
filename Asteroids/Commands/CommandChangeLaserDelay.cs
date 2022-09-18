namespace Asteroids;

internal class CommandChangeLaserDelay : ICommand
{
    private readonly IArmedObject armedObject;
    private readonly int delta;
    private readonly int oldDelay;

    public CommandChangeLaserDelay(IArmedObject armedObject, int delta)
    {
        this.armedObject = armedObject;
        this.delta       = delta;

        this.oldDelay = this.armedObject.DelayOfLaser;
    }

    public void Execute()
    {
        int newDelay = this.oldDelay + this.delta;

        this.armedObject.DelayOfLaser = newDelay switch
        {
            <= 0  => 0,
            >= 150 => 150,
            _     => newDelay
        };
    }

    public void Undo()
    {
        this.armedObject.DelayOfLaser = this.oldDelay;
    }
}