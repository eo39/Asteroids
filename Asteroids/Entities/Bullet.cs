namespace Asteroids;

internal class Bullet : GameObject
{
    public Bullet(int positionX, int positionY)
    {
        this.Bitmap     = Properties.Resources.Bullet;
        this.ObjectType = ObjectType.Bullet;
        this.PositionX  = positionX;
        this.PositionY  = positionY;
        this.OffsetX    = 8;
        this.OffsetY    = 0;
        this.Health     = 1;
        this.Size       = 14;
    }

    public override void Update(Game game)
    {
        game.CommandManager.ExecuteCommand(new CommandMove(this));
    }
}