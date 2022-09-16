namespace Asteroids;

internal class Meteor : GameObject
{
    public Meteor(int positionX, int positionY)
    {
        this.Bitmap     = Properties.Resources.Meteor;
        this.ObjectType = ObjectType.Meteor;
        this.PositionX  = positionX;
        this.PositionY  = positionY;
        this.OffsetX    = -10;
        this.OffsetY    = 4;
        this.Size       = 160;
        this.Health     = 10;
    }

    public override void Update(Game game)
    {
        game.CommandManager.ExecuteCommand(new CommandMove(this));
    }
}