namespace Asteroids;

internal class Laser : GameObject, ITemporaryObject
{
    private int lifetime;
    
    public Laser(CreationParams creationParams) : base(creationParams)
    {
        this.ObjectType = ObjectType.Laser;
        this.Health     = 1000;
        this.Size       = 500;
    }

    public override bool IsDestroyed()
    {
        return base.IsDestroyed() || this.lifetime > 10;
    }

    public override void Update(Game game)
    {
        game.CommandManager.ExecuteCommand(new CommandChangeLifetime(this));
    }

    public override bool IntersectsWith(GameObject gameObject)
    {
        var gameObjectPosition = new Point(gameObject.PositionX, gameObject.PositionY);

        var thisPosition = new Point(this.PositionX, this.PositionY);
        Point thisFinishPosition = this.GetFinishPosition();

        if (gameObject.PositionX + gameObject.Size / 2 < this.PositionX &&
            gameObject.PositionX + gameObject.Size / 2 < thisFinishPosition.X)
            return false;
        
        if (gameObject.PositionY + gameObject.Size / 2 < this.PositionY &&
            gameObject.PositionY + gameObject.Size / 2 < thisFinishPosition.Y)
            return false;
        
        if (gameObject.PositionX - gameObject.Size / 2 > this.PositionX &&
            gameObject.PositionX - gameObject.Size / 2 > thisFinishPosition.X)
            return false;
        
        if (gameObject.PositionY - gameObject.Size / 2 > this.PositionY &&
            gameObject.PositionY - gameObject.Size / 2 > thisFinishPosition.Y)
            return false;

        double distanceToObject = Utils.GetDistanceFromPointToLine(gameObjectPosition, thisPosition, thisFinishPosition);

        return distanceToObject <= (this.Size + gameObject.Size) / 2.0;
    }
    
    public void IncreaseLifetime()
    {
        this.lifetime++;
    }

    public void DecreaseLifetime()
    {
        this.lifetime--;
    }

    public override void Draw(Graphics graphics)
    {
        var startPosition = new Point(this.PositionX, this.PositionY);

        graphics.DrawLine(Pens.Red, startPosition, this.GetFinishPosition());
    }

    private Point GetFinishPosition()
    {
        double rotationAngle = this.RotationDegrees.ToRadians();
        
        double offsetX = this.Size * Math.Cos(rotationAngle);
        if (rotationAngle > 180) 
            offsetX *= -1;

        double offsetY = this.Size * Math.Sin(rotationAngle);
        if (rotationAngle is > 90 and < 270) 
            offsetY *= -1;

        return new Point((int)offsetX + this.PositionX, (int)offsetY + this.PositionY);
    }
}