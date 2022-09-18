namespace Asteroids;

internal class CommandManager
{
    private readonly List<List<ICommand>> commandRosters;

    public CommandManager()
    {
        this.commandRosters = new List<List<ICommand>>();
    }

    public void ExecuteCommand(ICommand command)
    {
        if (this.commandRosters.Count == 0)
            return;

        command.Execute();
        this.commandRosters.Last().Add(command);
    }

    public void CreateNewRoster()
    {
        this.commandRosters.Add(new List<ICommand>());
    }

    public void UndoLastRoster()
    {
        if (this.commandRosters.Count == 0)
            return;

        foreach (ICommand command in this.commandRosters.Last())
            command.Undo();

        this.commandRosters.RemoveAt(this.commandRosters.Count - 1);
    }
}