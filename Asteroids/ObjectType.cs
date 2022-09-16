namespace Asteroids;

[Flags]
internal enum ObjectType
{
    PlayerShip = 1,
    Bullet     = 2,
    EnemyShip  = 4,
    Meteor     = 8,
    Chip       = 16
}