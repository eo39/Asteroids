namespace Asteroids;

internal class CommandChangeLifetime : ICommand
{
    private readonly ITemporaryObject temporaryObject;

    public CommandChangeLifetime(ITemporaryObject temporaryObject)
    {
        this.temporaryObject = temporaryObject;
    }

    public void Execute()
    {
        this.temporaryObject.IncreaseLifetime();
    }

    public void Undo()
    {
        this.temporaryObject.DecreaseLifetime();
    }
}