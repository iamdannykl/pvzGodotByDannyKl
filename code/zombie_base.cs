using Godot;
using System;

public partial class zombie_base : CharacterBody2D
{
    [Export] public float spd;
    public bool isUpdt;
    public Vector2 weiZhi;
    [Export] public ZomType zomType;
    private AnimationTree _animationTree;
    //AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        //animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        //animationPlayer.SpeedScale = 0.35f;
        _animationTree.Set("parameters/StateMachine/conditions/isBegin", true);
    }

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
