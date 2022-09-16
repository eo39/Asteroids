namespace Asteroids;

internal class EnemyShip : GameObject
{
    public EnemyShip(int positionX, int positionY)
    {
        this.Bitmap     = Properties.Resources.EnemyShip;
        this.ObjectType = ObjectType.EnemyShip;
        this.PositionX  = positionX;
        this.PositionY  = positionY;
        this.OffsetX    = -6;
        this.OffsetY    = 0;
        this.Health     = 3;
        this.Size       = 80;
    }

    public override void Update(Game game)
    {
        /*
        GameObject playerShip =
            game.GameObjects.FirstOrDefault(gameObject => gameObject.ObjectType == ObjectType.PlayerShip);


        if (playerShip != null && Math.Abs(PositionY - playerShip.PositionY) <= playerShip.Size && DelayOfShot <= 0)
        {
           game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects,
                new BomberShipBullet(PositionX - Size, PositionY)));
            ReloadWeapon();
        }*/
        
        game.CommandManager.ExecuteCommand(new CommandMove(this));
    }
}