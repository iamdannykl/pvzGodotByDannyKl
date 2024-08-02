using Godot;
using System;

public partial class sunCreator : Node2D
{
    [Export] public PackedScene sun;
    [Export] public Timer timer;
    public void createsun()
    {
        sun sunIns = sun.Instantiate<sun>();
        AddChild(sunIns);
    }
}
