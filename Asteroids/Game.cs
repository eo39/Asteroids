namespace Asteroids;

using States;

internal class Game
{
    public int GameFieldWidth { get; private set; }
    public int GameFieldHeight { get; private set; }
    public int Score;

    public List<GameObject> GameObjects { get; private set; }
    public CommandManager CommandManager { get; private set; }
    public NormalState? NormalState { get; private set; }
    public ReverseState? ReverseState { get; private set; }
    public IState? CurrentState { private get; set; }

    private static readonly Random Random = new();
    private readonly Font font = new("Arial", 15);
    
    private DefeatState? defeatState;
    private PlayerShip PlayerShip => (PlayerShip)this.GameObjects.First();

    public void StartGame(int gameFieldWidth, int gameFieldHeight)
    {
        this.GameFieldWidth  = gameFieldWidth;
        this.GameFieldHeight = gameFieldHeight;
        this.Score           = 0;

        this.GameObjects     = new List<GameObject> { new PlayerShip(gameFieldWidth) };
        this.CommandManager  = new CommandManager();
        
        this.NormalState     = new NormalState(this);
        this.ReverseState    = new ReverseState(this);
        this.defeatState     = new DefeatState(this);
        this.CurrentState    = this.NormalState;
    }

    public void TickTimer()
    {
        this.CurrentState?.UpdateGame();
    }

    public void Update()
    {
        this.CommandManager.CreateNewRoster();
        this.UpdateGameObjects();

        if (this.GameObjects.Count(gameObject => gameObject.ObjectType != ObjectType.Bullet) < 2)
            this.GenerateEnemies();

        if (this.PlayerShip.Health < 1)
            this.CurrentState = this.defeatState;
    }

    private void UpdateGameObjects()
    {
        // In that cycle method "Update" change collection
        for (int index = 0; index < this.GameObjects.Count; index++) 
            this.GameObjects[index].Update(this);

        for (int i = 0; i < this.GameObjects.Count; i++)
        {
            GameObject gameObject = this.GameObjects[i];

            /*
            if (gameObject.PositionY + gameObject.Size / 2 >= ground.PositionY + 5 &&
                CanIntersect(gameObject, ground))
            {
                if (gameObject.ObjectType == ObjectType.PlayerShip)
                    CurrentState = defeatState;
                else
                {
                    CommandManager.ExecuteCommand(new CommandDeath(GameObjects, gameObject));
                    CommandManager.ExecuteCommand(new CommandCreate(GameObjects, new Blast(new Point(
                        gameObject.PositionX, gameObject.PositionY + gameObject.Size / 2))));
                    continue;
                }
            }
            */

            for (int j = i; j < this.GameObjects.Count; j++)
            {
                GameObject nextGameObject = this.GameObjects[j];

                if (this.CanIntersect(gameObject, nextGameObject) &&
                    gameObject.IntersectsWith(nextGameObject) &&
                    nextGameObject.Health > 0)
                {
                    int amountOfDamage = Math.Min(gameObject.Health, nextGameObject.Health);

                    this.CommandManager.ExecuteCommand(new CommandTakeDamage(gameObject, amountOfDamage));
                    this.CommandManager.ExecuteCommand(new CommandTakeDamage(nextGameObject, amountOfDamage));

                    /*
                    if (gameObject.Health <= 0 || nextGameObject.Health <= 0)
                        CommandManager.ExecuteCommand(new CommandCreate(GameObjects,
                            new Blast(gameObject.GetMiddleOfVector(nextGameObject))));
                            */

                    switch (gameObject.ObjectType)
                    {
                        case ObjectType.PlayerShip:
                        case ObjectType.Bullet:
                            if (nextGameObject.Health <= 0 && nextGameObject.ObjectType == ObjectType.EnemyShip)
                                this.CommandManager.ExecuteCommand(new CommandChangeScore(this));
                            break;
                        case ObjectType.EnemyShip:
                            if (gameObject.Health <= 0 && nextGameObject.ObjectType == ObjectType.Bullet)
                                this.CommandManager.ExecuteCommand(new CommandChangeScore(this));
                            break;
                    }
                }
            }

            if (gameObject.ObjectType != ObjectType.PlayerShip && gameObject.Health == 0)
                this.CommandManager.ExecuteCommand(new CommandDeath(this.GameObjects, gameObject));
        }
    }


    public void StartMovingPlayerShip(MoveMode moveMode)
    {
        this.PlayerShip.StartMoving(moveMode);
    }

    public void StopMovingPlayerShip(MoveMode moveMode)
    {
        this.PlayerShip.StopMoving(moveMode);
    }

    public void ClearPlayerShipsMoveMode()
    {
        this.PlayerShip.ClearMoveMode();
    }

    public void StartPlayerShipShooting()
    {
        this.PlayerShip.StartShooting();
    }

    public void StopPlayerShipShooting()
    {
        this.PlayerShip.StopShooting();
    }

    private void GenerateEnemies()
    {
        int generatedObjectsCount = Random.Next(1, 3);

        for (int i = 0; i < generatedObjectsCount; i++)
            switch (Random.Next(1, 5))
            {
                case 1:
                case 2:
                    this.CommandManager.ExecuteCommand(new CommandCreate(this.GameObjects,
                        new EnemyShip(this.GameFieldWidth, Random.Next(100, this.GameFieldHeight - 350))));
                    break;
                case 3:
                    this.CommandManager.ExecuteCommand(new CommandCreate(this.GameObjects,
                        new Meteor(Random.Next(this.GameFieldWidth - 100, this.GameFieldWidth), 0)));
                    break;
            }
    }


    public void DownKey(Keys keyCode)
    {
        this.CurrentState?.DownKey(keyCode);
    }

    public void UpKey(Keys keyCode)
    {
        this.CurrentState?.UpKey(keyCode);
    }

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
        graphics.DrawString("Score: " + this.Score, this.font, Brushes.Black, this.GameFieldWidth - 165, 10);
        graphics.DrawString("Player's health: " + this.PlayerShip.Health, this.font, Brushes.Black, this.GameFieldWidth - 165, 30);

        if (this.CurrentState is DefeatState)
            graphics.DrawString("Press Shift to reverse time  \nPress R to restart",
                this.font, Brushes.Black, this.GameFieldWidth / 2 - 80, this.GameFieldHeight / 2);
    }

    private void DrawGameObjects(Graphics graphics)
    {
        foreach (GameObject gameObject in this.GameObjects)
            gameObject.Draw(graphics);
    }

    private readonly Dictionary<ObjectType, ObjectType> intersectTable = new()
    {
        [ObjectType.PlayerShip] = ObjectType.EnemyShip |
                                  ObjectType.Meteor |
                                  ObjectType.Chip,

        [ObjectType.Bullet] = ObjectType.EnemyShip |
                              ObjectType.Meteor |
                              ObjectType.Chip,

        [ObjectType.EnemyShip] = ObjectType.PlayerShip |
                                 ObjectType.Bullet |
                                 ObjectType.Meteor,

        [ObjectType.Chip] = ObjectType.PlayerShip |
                            ObjectType.Bullet |
                            ObjectType.Meteor,

        [ObjectType.Meteor] = ObjectType.PlayerShip |
                              ObjectType.Bullet
    };

    private bool CanIntersect(GameObject firstGameObject, GameObject secondGameObject)
    {
        return this.intersectTable[firstGameObject.ObjectType].HasFlag(secondGameObject.ObjectType);
    }
}