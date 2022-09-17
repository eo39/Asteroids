﻿namespace Asteroids;

internal abstract class GameObject
{
    protected Bitmap Bitmap;

    public ObjectType ObjectType { get; protected set; }
    public int PositionX         { get; set; }
    public int PositionY         { get; set; }
    public int OffsetX           { get; protected set; }
    public int OffsetY           { get; protected set; }
    public int Health            { get; set; }
    public int Size              { get; protected set; }
    public int RotationAngle     { get; set; }

    public abstract void Update(Game game);

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
        graphics.RotateTransform(this.RotationAngle);
        graphics.TranslateTransform(-x, -y);
        graphics.DrawImage(this.Bitmap, 0, 0, this.Size, this.Size);
        
        return returnBitmap;
    }
}