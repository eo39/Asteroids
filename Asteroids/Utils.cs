namespace Asteroids;

internal static class Utils
{
    public static double GetAngleBetweenPoints(Point firstPoint, Point secondPoint)
    {
        double differenceX = secondPoint.X - firstPoint.X;
        double differenceY = secondPoint.Y - firstPoint.Y;
        
        double result = Math.Atan(differenceY / differenceX) * 180 / Math.PI;

        if (differenceX < 0)
            result += 180;

        if (result < 0)
            result = 360 + result;

        return result;
    }
}