namespace Asteroids;

internal class PlayerShip : GameObject
{
    private MoveMode playerMoveMode;
    private bool isPlayerShooting;
    private int delayOfShot;
    private bool isPlayerUsedLaser;
    
    public int DelayOfLaser { get; private set; }

    public PlayerShip(CreationParams creationParams) : base(creationParams)
    {
        this.Bitmap           = Properties.Resources.PlayerShip;
        this.ObjectType       = ObjectType.PlayerShip;
        
        this.Health           = 5;
        this.Size             = 80;
        this.isPlayerShooting = false;
        this.delayOfShot      = 0;

        this.isPlayerUsedLaser = false;
    }

    private void DecreaseWeaponsDelay()
    {
        if (this.delayOfShot > 0)
            this.delayOfShot--;
        
        if (this.DelayOfLaser > 0)
            this.DelayOfLaser--;
    }

    public override void Update(Game game)
    {
        if (this.playerMoveMode.HasFlag(MoveMode.Left)) 
            game.CommandManager.ExecuteCommand(new CommandRotate(this, angleOffset: -2));

        if (this.playerMoveMode.HasFlag(MoveMode.Right)) 
            game.CommandManager.ExecuteCommand(new CommandRotate(this, angleOffset: 2));

        double speedDelta = this.playerMoveMode.HasFlag(MoveMode.Up) ? 0.5 : -0.3;
        game.CommandManager.ExecuteCommand(new CommandChangeSpeed(this, speedDelta));

        base.Update(game);
        
        this.DecreaseWeaponsDelay();

        if (this.isPlayerShooting && this.delayOfShot <= 0)
        {
            var bulletPosition = this.GetBulletPosition();
            
            var bulletCreationParams = new CreationParams
            {
                PositionX       = bulletPosition.X,
                PositionY       = bulletPosition.Y,
                RotationDegrees = this.RotationDegrees
            };
            
            var bullet = new Bullet(bulletCreationParams);
            
            game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, bullet));
            
            this.delayOfShot = 15;
        }

        if (this.isPlayerUsedLaser && this.DelayOfLaser <= 0)
        {
            Point bulletPosition = this.GetBulletPosition();
            
            var bulletCreationParams = new CreationParams
            {
                PositionX       = bulletPosition.X,
                PositionY       = bulletPosition.Y,
                RotationDegrees = this.RotationDegrees
            };
            
            var bullet = new Laser(bulletCreationParams);
            
            game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, bullet));

            this.DelayOfLaser = 150;
        }
    }

    public void StartMoving(MoveMode moveMode)
    {
        switch (moveMode)
        {
            case MoveMode.Up:
                this.playerMoveMode |= MoveMode.Up;
                break;
            case MoveMode.Right:
                this.playerMoveMode |= MoveMode.Right;
                break;
            case MoveMode.Down:
                this.playerMoveMode |= MoveMode.Down;
                break;
            case MoveMode.Left:
                this.playerMoveMode |= MoveMode.Left;
                break;
        }
    }

    public void StopMoving(MoveMode moveMode)
    {
        switch (moveMode)
        {
            case MoveMode.Up:
                this.playerMoveMode ^= MoveMode.Up;
                break;
            case MoveMode.Right:
                this.playerMoveMode ^= MoveMode.Right;
                break;
            case MoveMode.Down:
                this.playerMoveMode ^= MoveMode.Down;
                break;
            case MoveMode.Left:
                this.playerMoveMode ^= MoveMode.Left;
                break;
        }
    }

    public void ClearMoveMode()
    {
        this.playerMoveMode = 0;
    }

    public void StartShooting()
    {
        this.isPlayerShooting = true;
    }

    public void StopShooting()
    {
        this.isPlayerShooting = false;
    }
    
    public void StartUsedLaser()
    {
        this.isPlayerUsedLaser = true;
    }

    public void StopUsedLaser()
    {
        this.isPlayerUsedLaser = false;
    }
    
    private Point GetBulletPosition()
    {
        double rotationAngle = this.RotationDegrees.ToRadians();

        int offsetX = (int) (Math.Cos(rotationAngle) * this.Size / 2);
        int offsetY = (int) (Math.Sin(rotationAngle) * this.Size / 2);

        return new Point(this.PositionX + offsetX, this.PositionY + offsetY);
    }
}