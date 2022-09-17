namespace Asteroids;

using System.Drawing.Drawing2D;

public partial class MainForm : Form
{
    private readonly Game game = new Game();

    public MainForm()
    {
        this.InitializeComponent();

        this.gameField.Paint += this.Draw;
        
        this.game.StartGame(this.gameField.Width, this.gameField.Height);
    }

    private void Draw(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        this.game.Draw(e.Graphics);
    }

    private void TickTimer(object sender, EventArgs e)
    {
        this.game.TickTimer();
        this.gameField.Refresh();
    }

    private void DownKey(object sender, KeyEventArgs e)
    {
        this.game.DownKey(e.KeyCode);
    }

    private void UpKey(object sender, KeyEventArgs e)
    {
        this.game.UpKey(e.KeyCode);
    }

    private void ClickHelp(object sender, EventArgs e)
    {
        MessageBox.Show("Move player's ship - WASD" +
                        "\nStart shooting - Space" +
                        "\nReverse - Shift ");
    }

    private void ClickPause(object sender, EventArgs e)
    {
        this.Timer.Enabled = !this.Timer.Enabled;
        this.pauseToolStripMenuItem.Text = this.Timer.Enabled ? "Pause" : "Continue";
    }
}