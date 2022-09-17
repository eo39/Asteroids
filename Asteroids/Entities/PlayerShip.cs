﻿namespace Asteroids;

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
        this.Health           = 5;
        this.Size             = 80;
        this.delayOfShot      = 0;
        this.isPlayerShooting = false;
        this.RotationDegrees   = 0;
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

        /*
        this.DecreaseDelayOfShot(15);

        if (this.isPlayerShooting && this.delayOfShot <= 0)
        {
            game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, new Bullet(this.PositionX + this.Size / 2, this.PositionY)));
            this.ReloadWeapon();
        }
        */
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
}