namespace Asteroids;

internal class Bullet : GameObject
{
    private int lifetime;
    
    public Bullet(CreationParams creationParams) : base(creationParams)
    {
        this.Bitmap          = Properties.Resources.Bullet;
        this.ObjectType      = ObjectType.Bullet;
        
        this.Speed           = 10;
        this.Health          = 1;
        this.Size            = 14;
        this.lifetime        = 0;
    }

    public override bool IsDestroyed()
    {
        return base.IsDestroyed() || this.lifetime > 100;
    }

    public override void Update(Game game)
    {
        this.lifetime++;
        
        base.Update(game);
    }
}