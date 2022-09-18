namespace Asteroids;

internal class PlayerShip : GameObject, IArmedObject
{
    private MoveMode playerMoveMode;
    private bool isPlayerShooting;
    private bool isPlayerUsedLaser;
    
    public int DelayOfShot { get; set; }
    public int DelayOfLaser { get; set; }

    public PlayerShip(CreationParams creationParams) : base(creationParams)
    {
        this.Bitmap           = Properties.Resources.PlayerShip;
        this.ObjectType       = ObjectType.PlayerShip;
        
        this.Health           = 1;
        this.Size             = 80;
        this.isPlayerShooting = false;

        this.isPlayerUsedLaser = false;
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
        this.UpdateWeapon(game);
    }

    private void UpdateWeapon(Game game)
    {
        game.CommandManager.ExecuteCommand(new CommandChangeShotgunDelay(this, -1));
        game.CommandManager.ExecuteCommand(new CommandChangeLaserDelay(this, -1));

        if (this.isPlayerShooting && this.DelayOfShot == 0)
        {
            var bulletPosition = this.GetArmoryPosition();
            
            var bulletCreationParams = new CreationParams
            {
                PositionX       = bulletPosition.X,
                PositionY       = bulletPosition.Y,
                RotationDegrees = this.RotationDegrees
            };
            
            var bullet = new Bullet(bulletCreationParams);
            game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, bullet));
            
            game.CommandManager.ExecuteCommand(new CommandChangeShotgunDelay(this, 15));
        }

        if (this.isPlayerUsedLaser && this.DelayOfLaser == 0)
        {
            Point laserPosition = this.GetArmoryPosition();
            
            var laserCreationParams = new CreationParams
            {
                PositionX       = laserPosition.X,
                PositionY       = laserPosition.Y,
                RotationDegrees = this.RotationDegrees
            };
            
            var laser = new Laser(laserCreationParams);
            game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, laser));
            
            game.CommandManager.ExecuteCommand(new CommandChangeLaserDelay(this, 150));
        }
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

    private Point GetArmoryPosition()
    {
        double rotationAngle = this.RotationDegrees.ToRadians();

        int offsetX = (int) (Math.Cos(rotationAngle) * this.Size / 2);
        int offsetY = (int) (Math.Sin(rotationAngle) * this.Size / 2);

        return new Point(this.PositionX + offsetX, this.PositionY + offsetY);
    }
}