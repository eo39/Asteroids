namespace Asteroids;

using System.Numerics;

internal abstract class GameObject
{
    protected Bitmap? Bitmap;
    public ObjectType ObjectType { get; protected init; }
    
    public int PositionX         { get; set; }
    public int PositionY         { get; set; }
    public int RotationDegrees   { get; set; }
    
    public double Speed          { get; set; }
    public int Health            { get; set; }
    public int Size              { get; protected init; }

    protected GameObject(CreationParams creationParams)
    {
        this.PositionX       = creationParams.PositionX;
        this.PositionY       = creationParams.PositionY;
        this.RotationDegrees = creationParams.RotationDegrees;
    }

    public virtual void Update(Game game)
    {
        game.CommandManager.ExecuteCommand(new CommandMove(this));

        if (this.PositionX > game.GameFieldWidth)
            game.CommandManager.ExecuteCommand(new CommandTeleport(this, 0, this.PositionY));
        
        if (this.PositionX < 0)
            game.CommandManager.ExecuteCommand(new CommandTeleport(this, game.GameFieldWidth, this.PositionY));
        
        if (this.PositionY > game.GameFieldHeight)
            game.CommandManager.ExecuteCommand(new CommandTeleport(this, this.PositionX, 0));
        
        if (this.PositionY < 0)
            game.CommandManager.ExecuteCommand(new CommandTeleport(this, this.PositionX, game.GameFieldHeight));
    }

    public virtual bool IsDestroyed()
    {
        return this.Health == 0;
    }

    public virtual bool IntersectsWith(GameObject gameObject)
    {
        if (gameObject is Laser laser)
            return laser.IntersectsWith(this);
        
        double sqrDistanceToObject = Utils.GetSqrDistanceToObject(this.PositionX, this.PositionY, gameObject.PositionX, gameObject.PositionY);
        
        return sqrDistanceToObject <= (this.Size + gameObject.Size) * (this.Size + gameObject.Size) / 4.0;
    }

    public Vector2 GetNextTickOffset()
    {
        double rotationAngle = this.RotationDegrees.ToRadians();

        float offsetX = (float) (Math.Cos(rotationAngle) * this.Speed);
        float offsetY = (float) (Math.Sin(rotationAngle) * this.Speed);

        return new Vector2(offsetX, offsetY);
    }

    public virtual void Draw(Graphics graphics)
    {
        int x = this.PositionX - this.Size / 2;
        int y = this.PositionY - this.Size / 2;
        
       Bitmap rotatedBitmap = this.RotateImage();

       graphics.DrawImage(rotatedBitmap, x, y, this.Size, this.Size);
    }
    
    private Bitmap RotateImage()
    {
        Bitmap returnBitmap = new Bitmap(this.Size, this.Size);
        using Graphics graphics = Graphics.FromImage(returnBitmap);
        
        float x = this.Size / 2f;
        float y = this.Size / 2f;

        graphics.TranslateTransform(x, y);
        graphics.RotateTransform(this.RotationDegrees);
        graphics.TranslateTransform(-x, -y);
        graphics.DrawImage(this.Bitmap, 0, 0, this.Size, this.Size);
        
        return returnBitmap;
    }
}