namespace Asteroids.States;

internal class NormalState : IState
{
    private readonly Game game;

    public NormalState(Game game)
    {
       this.game = game;
    }

    public void DownKey(Keys keyCode)
    {
        switch (keyCode)
        {
            case Keys.W:
                this.game.StartMovingPlayerShip(MoveMode.Up);
                break;
            case Keys.D:
                this.game.StartMovingPlayerShip(MoveMode.Right);
                break;
            case Keys.S:
                this.game.StartMovingPlayerShip(MoveMode.Down);
                break;
            case Keys.A:
                this.game.StartMovingPlayerShip(MoveMode.Left);
                break;
            case Keys.Space:
                this.game.StartPlayerShipShooting();
                break;
            case Keys.ShiftKey:
                this.game.SetCurrentState(this.game.ReverseState);
                break;
        }
    }

    public void UpKey(Keys keyCode)
    {
        switch (keyCode)
        {
            case Keys.W:
                this.game.StopMovingPlayerShip(MoveMode.Up);
                break;
            case Keys.D:
                this.game.StopMovingPlayerShip(MoveMode.Right);
                break;
            case Keys.S:
                this.game.StopMovingPlayerShip(MoveMode.Down);
                break;
            case Keys.A:
                this.game.StopMovingPlayerShip(MoveMode.Left);
                break;
            case Keys.Space:
                this.game.StopPlayerShipShooting();
                break;
        }
    }

    public void UpdateGame()
    {
        this.game.Update();
    }
}