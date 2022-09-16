namespace Asteroids;

internal class CommandCreate : ICommand
{
    private readonly List<GameObject> gameObjects;
    private readonly GameObject gameObject;

    public CommandCreate(List<GameObject> gameObjects, GameObject gameObject)
    {
        this.gameObjects = gameObjects;
        this.gameObject  = gameObject;
    }

    public void Execute()
    {
        this.gameObjects.Add(this.gameObject);
    }

    public void Undo()
    {
        this.gameObjects.Remove(this.gameObject);
    }
}