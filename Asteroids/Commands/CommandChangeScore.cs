namespace Asteroids;

internal class CommandChangeScore : ICommand
{
    private readonly Game game;

    public CommandChangeScore(Game game)
    {
        this.game = game;
    }

    public void Execute()
    {
        this.game.Score++;
    }

    public void Undo()
    {
        this.game.Score--;
    }
}