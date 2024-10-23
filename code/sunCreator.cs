using Godot;
using System;

public partial class sunCreator : Node2D
{
    [Export] public PackedScene sun;
    [Export] public Timer timer;
    [Export] public Timer first;
    Random random;
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
    public void createsunRandom()
    {
        int randomInt = random.Next(0, 9);
        Vector2 sunPos = new Vector2(GridSys.Instance.zuoxia.GlobalPosition.X + GridSys.Instance.XjianGe * randomInt, GlobalPosition.Y);
        sun sunIns = sun.Instantiate<sun>();
        sunIns.GlobalPosition = sunPos;
        int randomIntY = random.Next(0, 5);
        AddChild(sunIns);
        sunIns.fallSun(randomIntY);
        timer.Start();
    }
    public void createsunFirstRandom()
    {
        random = new Random();
        int randomInt = random.Next(0, 9);
        Vector2 sunPos = new Vector2(GridSys.Instance.zuoxia.GlobalPosition.X + GridSys.Instance.XjianGe * randomInt, GlobalPosition.Y);
        timer.Start();
        sun sunIns = sun.Instantiate<sun>();
        sunIns.GlobalPosition = sunPos;
        int randomIntY = random.Next(0, 5);
        AddChild(sunIns);
        sunIns.fallSun(randomIntY);
    }
}
