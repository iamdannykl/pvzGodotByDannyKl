using Godot;
using System;

public partial class hangSys : Node2D
{
    public static hangSys Instance;

    public override void _Ready()
    {
        Instance = this;
    }
}
