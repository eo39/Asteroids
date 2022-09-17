namespace Asteroids;

using System.Numerics;

internal abstract class GameObject
{
    protected Bitmap Bitmap;

    public ObjectType ObjectType { get; protected set; }
    public int PositionX         { get; set; }
    public int PositionY         { get; set; }
    public double Speed          { get; set; }
    public int Health            { get; set; }
    public int Size              { get; protected set; }
    public int RotationDegree    { get; set; }

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

    public bool IntersectsWith(GameObject gameObject)
    {
        double distanceToObject = this.GetSqrDistanceToObject(gameObject.PositionX, gameObject.PositionY);
        return distanceToObject <= (this.Size + gameObject.Size) * (this.Size + gameObject.Size) / 4.0;
    }

    public Point GetMiddleOfVector(GameObject gameObject)
    {
        int positionX = (this.PositionX + gameObject.PositionX) / 2;
        int positionY = (this.PositionY + gameObject.PositionY) / 2;

        return new Point(positionX, positionY);
    }

    private double GetSqrDistanceToObject(int objectX, int objectY)
    {
        double componentX = Math.Pow(this.PositionX - objectX, 2);
        double componentY = Math.Pow(this.PositionY - objectY, 2);

        return componentX + componentY;
    }

    public void Draw(Graphics graphics)
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
        graphics.RotateTransform(this.RotationDegree);
        graphics.TranslateTransform(-x, -y);
        graphics.DrawImage(this.Bitmap, 0, 0, this.Size, this.Size);
        
        return returnBitmap;
    }

    public Vector2 GetNewPosition()
    {
        double rotationAngle = Math.PI * this.RotationDegree / 180.0;

        float offsetX = (float) (Math.Cos(rotationAngle) * this.Speed);
        float offsetY = (float) (Math.Sin(rotationAngle) * this.Speed);

        return new Vector2(offsetX, offsetY);
    }
}