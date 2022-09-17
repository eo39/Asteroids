namespace Asteroids;

internal class Meteor : GameObject
{
    public Meteor(int positionX, int positionY)
    {
        this.Bitmap          = Properties.Resources.Meteor;
        this.ObjectType      = ObjectType.Meteor;
        
        this.PositionX       = positionX;
        this.PositionY       = positionY;
        
        this.RotationDegrees = 135;
        this.Speed           = 5;
        this.Health          = 10;
        this.Size            = 80;
    }
}