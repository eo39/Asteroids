namespace Asteroids;

internal class CommandCreate : ICommand
{
    private readonly List<GameObject> gameObjects;
    private readonly GameObject[] gameObjectsToCreate;

    public CommandCreate(List<GameObject> gameObjects, GameObject gameObject)
    {
        this.gameObjects         = gameObjects;
        this.gameObjectsToCreate = new[] { gameObject };
    }
    
    public CommandCreate(List<GameObject> gameObjects, GameObject[] gameObjectsToCreate)
    {
        this.gameObjects         = gameObjects;
        this.gameObjectsToCreate = gameObjectsToCreate;
    }

    public void Execute()
    {
        this.gameObjects.AddRange(this.gameObjectsToCreate);
    }

    public void Undo()
    {
        foreach (var gameObject in this.gameObjectsToCreate) 
            this.gameObjects.Remove(gameObject);
    }
}