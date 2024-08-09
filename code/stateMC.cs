using Godot;
using System;

public partial class stateMC : AnimationTree
{
    [Export] public bool isBegin;
    [Export] public bool isEat;
    [Export] public bool isDb;
    [Export] public bool isDead;
    public int nowJieDuan;
    public override void _Ready()
    {
    }
}
