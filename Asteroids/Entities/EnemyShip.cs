namespace Asteroids;

internal class EnemyShip : GameObject
{
    public EnemyShip(int positionX, int positionY)
    {
        this.Bitmap          = Properties.Resources.EnemyShip;
        this.ObjectType      = ObjectType.EnemyShip;
        
        this.PositionX       = positionX;
        this.PositionY       = positionY;
        
        this.RotationDegrees = 135;
        this.Speed           = 5;
        this.Health          = 1;
        this.Size            = 80;
    }

    public override void Update(Game game)
    {
        Point thisObjectPosition = new Point(this.PositionX, this.PositionY);
        Point playerShipPosition = game.GetPlayerShipPosition();

        int angleBetweenPoints = (int)Utils.GetAngleBetweenPoints(thisObjectPosition, playerShipPosition);
        int angleOffset = angleBetweenPoints - this.RotationDegrees;
        
        game.CommandManager.ExecuteCommand(new CommandRotate(this, angleOffset));

        base.Update(game);
    }
}