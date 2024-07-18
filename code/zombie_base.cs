using Godot;
using System;

public partial class zombie_base : CharacterBody2D
{
    [Export] public float spd;
    public bool isUpdt;
    public Vector2 weiZhi;
    [Export] public ZomType zomType;
    public override void _Process(double delta)
    {
        if (isUpdt)
        {
            Velocity = new Vector2(-spd, 0);
            MoveAndSlide();
        }
    }
    public void placed()
    {
        isUpdt = false;
    }
    public void crtIt()
    {
        isUpdt = true;
    }
}
