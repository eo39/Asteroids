namespace Asteroids;

internal static class Utils
{
    public static double GetAngleBetweenPoints(Point firstPoint, Point secondPoint)
    {
        double deltaX = secondPoint.X - firstPoint.X;
        double deltaY = secondPoint.Y - firstPoint.Y;
        
        double result = Math.Atan(deltaY / deltaX) * 180 / Math.PI;

        if (deltaX < 0)
            result += 180;

        if (result < 0)
            result = 360 + result;

        return result;
    }

    public static double GetDistanceFromPointToLine(Point point, Point lineStart, Point lineFinish)
    {
        double deltaX = lineFinish.X - lineStart.X;
        double deltaY = lineFinish.Y - lineStart.Y;

        double nominator = deltaY * point.X - deltaX * point.Y + lineFinish.X * lineStart.Y - lineFinish.Y * lineStart.X;
        double denominator = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        
        return Math.Abs(nominator) / denominator;
    }
    
    public static double GetSqrDistanceToObject(int positionX, int positionY, int objectX, int objectY)
    {
        double componentX = Math.Pow(positionX - objectX, 2);
        double componentY = Math.Pow(positionY - objectY, 2);

        return componentX + componentY;
    }

    public static double ToRadians(this int degrees) => Math.PI * degrees / 180.0;
}