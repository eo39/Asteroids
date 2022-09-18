namespace Asteroids;

internal class Meteor : GameObject
{
    public Meteor(CreationParams creationParams) : base(creationParams)
    {
        this.Bitmap          = Properties.Resources.Meteor;
        this.ObjectType      = ObjectType.Meteor;
        
        this.Speed           = 2;
        this.Health          = 2;
        this.Size            = 80;
    }
}