﻿namespace Asteroids;

internal class CommandRotate : ICommand
{
    private readonly GameObject gameObject;
    private readonly int rotationAngle;
    private readonly int angleOffset;

    public CommandRotate(GameObject gameObject, int angleOffset)
    {
        this.gameObject    = gameObject;
        this.rotationAngle = gameObject.RotationAngle;
        this.angleOffset   = angleOffset;
    }

    public void Execute()
    {
        int newRotationAngle = this.rotationAngle + this.angleOffset;

        this.gameObject.RotationAngle = newRotationAngle switch
        {
            < 0   => 360 - newRotationAngle,
            > 360 => newRotationAngle - 360,
            _     => newRotationAngle
        };
    }

    public void Undo()
    {
        this.gameObject.RotationAngle = this.rotationAngle;
    }
}