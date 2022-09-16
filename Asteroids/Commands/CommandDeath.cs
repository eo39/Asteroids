namespace Asteroids;

internal class CommandDeath : ICommand
{
    private readonly List<GameObject> gameObjects;
    private readonly GameObject gameObject;

    public CommandDeath(List<GameObject> gameObjects, GameObject gameObject)
    {
        this.gameObjects = gameObjects;
        this.gameObject  = gameObject;
    }

    public void Execute()
    {
        this.gameObjects.Remove(this.gameObject);
    }

    public void Undo()
    {
        this.gameObjects.Add(this.gameObject);
    }
}