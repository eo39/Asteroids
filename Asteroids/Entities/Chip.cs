namespace Asteroids;

internal class Chip : GameObject
{
    public Chip(int positionX, int positionY)
    {
        this.Bitmap          = Properties.Resources.Meteor;
        this.ObjectType      = ObjectType.Chip;
        
        this.PositionX       = positionX;
        this.PositionY       = positionY;
        
        this.RotationDegrees = 135;
        this.Speed           = 5;
        this.Health          = 1;
        this.Size            = 40;
    }
}