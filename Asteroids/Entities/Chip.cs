namespace Asteroids;

internal class Chip : GameObject
{
    public Chip(CreationParams creationParams) : base(creationParams)
    {
        this.Bitmap          = Properties.Resources.Meteor;
        this.ObjectType      = ObjectType.Chip;
        
        this.Speed           = 3;
        this.Health          = 1;
        this.Size            = 40;
    }
}