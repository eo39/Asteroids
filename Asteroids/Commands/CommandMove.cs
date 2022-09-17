namespace Asteroids;

using System.Numerics;

internal class CommandMove : ICommand
{
    private readonly GameObject gameObject;
    private readonly Vector2 offset;

    public CommandMove(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.offset     = gameObject.GetNewPosition();
    }

    public void Execute()
    {
        this.gameObject.PositionX += (int) this.offset.X;
        this.gameObject.PositionY += (int) this.offset.Y;
    }

    public void Undo()
    {
        this.gameObject.PositionX -= (int) this.offset.X;
        this.gameObject.PositionY -= (int) this.offset.Y;
    }
}