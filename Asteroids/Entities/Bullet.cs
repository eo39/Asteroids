namespace Asteroids;

internal class Bullet : GameObject
{
    public Bullet(int positionX, int positionY)
    {
        this.Bitmap     = Properties.Resources.Bullet;
        this.ObjectType = ObjectType.Bullet;
        this.PositionX  = positionX;
        this.PositionY  = positionY;
        this.Health     = 1;
        this.Size       = 14;
    }
}