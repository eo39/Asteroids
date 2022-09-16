namespace Asteroids;

internal class CommandTakeDamage : ICommand
{
    private readonly GameObject gameObject;
    private readonly int damage;

    public CommandTakeDamage(GameObject gameObject, int damage)
    {
        this.gameObject = gameObject;
        this.damage     = damage;
    }

    public void Execute()
    {
        this.gameObject.Health -= this.damage;
    }

    public void Undo()
    {
        this.gameObject.Health += this.damage;
    }
}