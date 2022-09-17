namespace Asteroids;

internal class Meteor : GameObject
{
    public Meteor(int positionX, int positionY)
    {
        this.Bitmap         = Properties.Resources.Meteor;
        this.ObjectType     = ObjectType.Meteor;
        this.PositionX      = positionX;
        this.PositionY      = positionY;
        this.Speed          = 5;
        this.RotationDegree = 180;
        this.Size           = 80;
        this.Health         = 10;
    }
}