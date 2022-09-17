namespace Asteroids;

internal class PlayerShip : GameObject
{
    private MoveMode playerMoveMode;
    private bool isPlayerShooting;
    private int delayOfShot;

    public PlayerShip()
    {
        this.Bitmap           = Properties.Resources.PlayerShip;
        this.ObjectType       = ObjectType.PlayerShip;
        
        this.PositionX        = 100;
        this.PositionY        = 375;
        
        this.RotationDegrees  = 0;
        this.Health           = 5;
        this.Size             = 80;
        
        this.delayOfShot      = 0;
        this.isPlayerShooting = false;
    }

    private void DecreaseDelayOfShot(int valueOfDecrease)
    {
        if (this.delayOfShot > 0)
            this.delayOfShot -= valueOfDecrease;
    }

    private void ReloadWeapon()
    {
        this.delayOfShot = 150;
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
        
        this.DecreaseDelayOfShot(15);

        if (this.isPlayerShooting && this.delayOfShot <= 0)
        {
            var bulletPosition = this.GetBulletPosition();
            var bullet = new Bullet(bulletPosition.X, bulletPosition.Y, this.RotationDegrees);
            
            game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, bullet));
            
            this.ReloadWeapon();
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
    
    private Point GetBulletPosition()
    {
        double rotationAngle = Math.PI * this.RotationDegrees / 180.0;

        int offsetX = (int) (Math.Cos(rotationAngle) * this.Size / 2);
        int offsetY = (int) (Math.Sin(rotationAngle) * this.Size / 2);

        return new Point(this.PositionX + offsetX, this.PositionY + offsetY);
    }

}