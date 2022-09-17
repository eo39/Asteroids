namespace Asteroids;

internal class CommandTeleport : ICommand
{
    private readonly GameObject gameObject;
    
    private readonly int newPositionX;
    private readonly int newPositionY;
    
    private readonly int oldPositionX;
    private readonly int oldPositionY;
    
    public CommandTeleport(GameObject gameObject, int newPositionX, int newPositionY)
    {
        this.gameObject   = gameObject;
        this.newPositionX = newPositionX;
        this.newPositionY = newPositionY;

        this.oldPositionX = gameObject.PositionX;
        this.oldPositionY = gameObject.PositionY;
    }

    public void Execute()
    {
        this.gameObject.PositionX = this.newPositionX;
        this.gameObject.PositionY = this.newPositionY;
    }

    public void Undo()
    {
        this.gameObject.PositionX = this.oldPositionX;
        this.gameObject.PositionY = this.oldPositionY;
    }
}