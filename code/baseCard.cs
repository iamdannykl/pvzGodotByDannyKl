using Godot;
using System;
using System.Diagnostics;

public partial class baseCard : Area2D
{
    [Export] public AnimatedSprite2D anim;
    public override void _Ready()
    {
        base._Ready();
        anim.SpeedScale = 0;
    }
    public void placed()
    {
        anim.SpeedScale = 1;
    }
}
