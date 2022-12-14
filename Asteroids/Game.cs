namespace Asteroids;

using States;

internal partial class Game
{
    public int GameFieldWidth { get; private set; }
    public int GameFieldHeight { get; private set; }
    public int Score;

    public List<GameObject> GameObjects { get; private set; }
    public CommandManager CommandManager { get; private set; }
    public NormalState? NormalState { get; private set; }
    public ReverseState? ReverseState { get; private set; }
    
    private static readonly Random Random = new();

    private IState? currentState;
    private DefeatState? defeatState;
    private PlayerShip PlayerShip => (PlayerShip)this.GameObjects.First();

    public void StartGame(int gameFieldWidth, int gameFieldHeight)
    {
        this.GameFieldWidth  = gameFieldWidth;
        this.GameFieldHeight = gameFieldHeight;
        this.Score           = 0;

        var playerShipCreationParams = new CreationParams
        {
            PositionX = this.GameFieldWidth / 2,
            PositionY = this.GameFieldHeight / 2
        };
        
        this.GameObjects     = new List<GameObject> { new PlayerShip(playerShipCreationParams) };
        
        this.CommandManager  = new CommandManager();
        
        this.NormalState     = new NormalState(this);
        this.ReverseState    = new ReverseState(this);
        this.defeatState     = new DefeatState(this);
        this.currentState    = this.NormalState;
    }

    public void TickTimer()
    {
        this.currentState?.UpdateGame();
    }

    public void Update()
    {
        this.CommandManager.CreateNewRoster();
        this.UpdateGameObjects();

        if (this.GameObjects.Count(gameObject => gameObject.ObjectType != ObjectType.Bullet) < 2)
          this.GenerateEnemies();

        if (this.PlayerShip.IsDestroyed())
            this.currentState = this.defeatState;
    }

    private void UpdateGameObjects()
    {
        // In that cycle method "Update" change collection
        for (int index = 0; index < this.GameObjects.Count; index++) 
            this.GameObjects[index].Update(this);

        for (int i = 0; i < this.GameObjects.Count; i++)
        {
            GameObject gameObject = this.GameObjects[i];

            for (int j = i; j < this.GameObjects.Count; j++)
            {
                GameObject nextGameObject = this.GameObjects[j];

                if (this.CanIntersect(gameObject, nextGameObject) &&
                    gameObject.IntersectsWith(nextGameObject) &&
                    nextGameObject.Health != 0)
                {
                    int amountOfDamage = Math.Min(gameObject.Health, nextGameObject.Health);
                    
                    this.CommandManager.ExecuteCommand(new CommandTakeDamage(gameObject, amountOfDamage));
                    this.CommandManager.ExecuteCommand(new CommandTakeDamage(nextGameObject, amountOfDamage));
                    
                    switch (gameObject.ObjectType)
                    {
                        case ObjectType.PlayerShip:
                        case ObjectType.Bullet:
                            if (nextGameObject.Health == 0 && nextGameObject.ObjectType == ObjectType.EnemyShip)
                                this.CommandManager.ExecuteCommand(new CommandChangeScore(this));
                            break;
                        case ObjectType.EnemyShip:
                            if (gameObject.Health == 0 && nextGameObject.ObjectType == ObjectType.Bullet)
                                this.CommandManager.ExecuteCommand(new CommandChangeScore(this));
                            break;
                    }
                }
            }

            if (gameObject.ObjectType != ObjectType.PlayerShip && gameObject.IsDestroyed())
            {
                this.CommandManager.ExecuteCommand(new CommandDeath(this.GameObjects, gameObject));

                if (gameObject.ObjectType == ObjectType.Meteor)
                {
                    var firstChipCreationParams = new CreationParams
                    {
                        PositionX       = gameObject.PositionX + 10,
                        PositionY       = gameObject.PositionY + 10,
                        RotationDegrees = gameObject.RotationDegrees + 45
                    };
                    var firstChip = new Chip(firstChipCreationParams);
                    
                    var secondChipCreationParams = new CreationParams
                    {
                        PositionX       = gameObject.PositionX - 10,
                        PositionY       = gameObject.PositionY - 10,
                        RotationDegrees = gameObject.RotationDegrees - 45 
                    };
                    var secondChip = new Chip(secondChipCreationParams);

                    GameObject[] chipsToCrete = { firstChip, secondChip };
                    
                    this.CommandManager.ExecuteCommand(new CommandCreate(this.GameObjects, chipsToCrete));
                }
            }
        }
    }

    public void SetCurrentState(IState? newState)
    {
        this.currentState = newState;
    }

    public Point GetPlayerShipPosition()
    {
        return new Point(this.PlayerShip.PositionX, this.PlayerShip.PositionY);
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
    
    public void StartPlayerShipUsedLaser()
    {
        this.PlayerShip.StartUsedLaser();
    }

    public void StopPlayerShipUsedLaser()
    {
        this.PlayerShip.StopUsedLaser();
    }

    private void GenerateEnemies()
    {
        int generatedObjectsCount = Random.Next(1, 3);

        for (int i = 0; i < generatedObjectsCount; i++)
        {
            var creationParams = new CreationParams
            {
                PositionX       = Random.Next(0, this.GameFieldWidth),
                PositionY       = Random.Next(0, this.GameFieldHeight),
                RotationDegrees = Random.Next(0, 360)
            };

            GameObject? generatedEnemy = Random.Next(1, 4) switch
            {
                1 or 2 => new EnemyShip(creationParams),
                3      => new Meteor(creationParams),
                _      => null
            };

            if (generatedEnemy != null)
                this.CommandManager.ExecuteCommand(new CommandCreate(this.GameObjects, generatedEnemy));
        }
    }
    
    public void DownKey(Keys keyCode)
    {
        this.currentState?.DownKey(keyCode);
    }

    public void UpKey(Keys keyCode)
    {
        this.currentState?.UpKey(keyCode);
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
                                 ObjectType.Laser,

        [ObjectType.Chip] = ObjectType.PlayerShip |
                            ObjectType.Bullet |
                            ObjectType.Laser,

        [ObjectType.Meteor] = ObjectType.PlayerShip |
                              ObjectType.Bullet |
                              ObjectType.Laser,
        
        [ObjectType.Laser] = ObjectType.EnemyShip |
                             ObjectType.Meteor |
                             ObjectType.Chip
    };

    private bool CanIntersect(GameObject firstGameObject, GameObject secondGameObject)
    {
        return this.intersectTable[firstGameObject.ObjectType].HasFlag(secondGameObject.ObjectType);
    }
}