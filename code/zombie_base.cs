using Godot;
using System;

public partial class zombie_base : CharacterBody2D
{
    [Export] public float spd;
    public override void _Process(double delta)
    {
        base._Process(delta);
        Velocity = new Vector2(-spd, 0);
        MoveAndSlide();
    }
}
