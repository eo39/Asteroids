namespace Asteroids;

internal interface IArmedObject
{
    public int DelayOfShot { get; set; }
    public int DelayOfLaser { get; set; }
}