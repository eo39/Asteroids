namespace Asteroids.States;

internal class DefeatState : IState
{
    private readonly Game game;

    public DefeatState(Game game)
    {
        this.game = game;
    }

    public void DownKey(Keys keyCode)
    {
        switch (keyCode)
        {
            case Keys.ShiftKey:
                this.game.CurrentState = this.game.ReverseState;
                break;
            case Keys.R:
                this.game.StartGame(this.game.GameFieldWidth, this.game.GameFieldHeight);
                break;
        }
    }

    public void UpKey(Keys keyCode)
    {
    }

    public void UpdateGame()
    {
    }
}