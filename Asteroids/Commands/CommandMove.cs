namespace Asteroids;

internal class CommandMove : ICommand
{
    private readonly GameObject gameObject;
    private readonly int offsetX;
    private readonly int offsetY;

    public CommandMove(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.offsetX    = gameObject.OffsetX;
        this.offsetY    = gameObject.OffsetY;
    }

    public void Execute()
    {
        this.gameObject.PositionX += this.offsetX;
        this.gameObject.PositionY += this.offsetY;
    }

    public void Undo()
    {
        this.gameObject.PositionX -= this.offsetX;
        this.gameObject.PositionY -= this.offsetY;
    }
}