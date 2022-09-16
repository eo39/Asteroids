namespace Asteroids;

internal interface IState
{
    public void DownKey(Keys keyCode);

    public void UpKey(Keys keyCode);

    public void UpdateGame();
}