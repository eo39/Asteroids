namespace Asteroids;

internal class ReverseState : IState
{
    private readonly Game game;
    private int reverseSpeed;

    public ReverseState(Game game)
    {
        this.game = game;
        this.reverseSpeed = 1;
    }

    public void DownKey(Keys keyCode)
    {
        switch (keyCode)
        {
            case Keys.Left when this.reverseSpeed > 1:
                this.reverseSpeed--;
                break;
            case Keys.Right when this.reverseSpeed < 8:
                this.reverseSpeed++;
                break;
        }
    }

    public void UpKey(Keys keyCode)
    {
        switch (keyCode)
        {
            case Keys.ShiftKey:
                this.game.SetCurrentState(this.game.NormalState);
                
                this.game.ClearPlayerShipsMoveMode();
                this.game.StopPlayerShipShooting();
                
                this.reverseSpeed = 1;
                break;
            case Keys.Left:
                this.reverseSpeed = this.reverseSpeed > 1 
                    ? this.reverseSpeed-- 
                    : this.reverseSpeed;
                break;
            case Keys.Right:
                this.reverseSpeed = this.reverseSpeed < 10 
                    ? this.reverseSpeed++ 
                    : this.reverseSpeed;
                break;
        }
    }

    public void UpdateGame()
    {
        for (int i = 0; i < this.reverseSpeed; i++)
            this.game.CommandManager.UndoLastRoster();
    }
}