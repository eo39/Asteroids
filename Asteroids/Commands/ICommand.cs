namespace Asteroids;

internal interface ICommand
{
    void Execute();
    void Undo();
}