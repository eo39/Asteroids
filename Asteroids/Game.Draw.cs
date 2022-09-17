namespace Asteroids;

using States;

internal partial class Game
{
    private readonly Font font = new("Arial", 15);
    
    public void Draw(Graphics graphics)
    {
        this.DrawBackground(graphics);
        this.DrawInterface(graphics);
        this.DrawGameObjects(graphics);
    }

    private void DrawBackground(Graphics graphics)
    {
        graphics.FillRectangle(Brushes.LightCyan, 0, 0, this.GameFieldWidth, this.GameFieldHeight);
    }

    private void DrawInterface(Graphics graphics)
    {
        graphics.DrawString($"Score: {this.Score}", this.font, Brushes.Black, 0, 10);
        graphics.DrawString($"Player's health: {this.PlayerShip.Health}", this.font, Brushes.Black, 0, 30);
        graphics.DrawString($"Rotate angle: {this.PlayerShip.RotationDegrees}", this.font, Brushes.Black, 0, 50);
        
#if DEBUG
        graphics.DrawString($"X: {this.PlayerShip.PositionX}, Y: {this.PlayerShip.PositionY}", this.font, Brushes.Black, 0, 100);
        graphics.DrawString($"Speed: {this.PlayerShip.Speed}", this.font, Brushes.Black, 0, 120);
#endif

        if (this.currentState is DefeatState)
            graphics.DrawString("Press Shift to reverse time  \nPress R to restart",
                this.font, Brushes.Black, this.GameFieldWidth / 2 - 80, this.GameFieldHeight / 2f);
    }

    private void DrawGameObjects(Graphics graphics)
    {
        foreach (GameObject gameObject in this.GameObjects)
            gameObject.Draw(graphics);
    }
}