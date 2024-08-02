using Godot;
using System;

public partial class sunCreator : Node2D
{
    [Export] public PackedScene sun;
    [Export] public Timer timer;
    [Export] public Timer first;
    public void createsun()
    {
        sun sunIns = sun.Instantiate<sun>();
        AddChild(sunIns);
    }
    public void createsunFirst()
    {
        timer.Start();
        sun sunIns = sun.Instantiate<sun>();
        AddChild(sunIns);
    }
}
