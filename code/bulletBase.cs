using Godot;
using System;
public partial class bulletBase : Area2D
{
    AnimatedSprite2D anim;
    [Export] public float bltSpd;
    bool canMove = true;
    public override void _Ready()
    {
        anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        anim.Connect("animation_finished", new Callable(this, nameof(desSelf)));
        Connect("area_entered", new Callable(this, nameof(hitZom)));
    }
    public void hitZom(Area2D area2D)
    {
        canMove = false;
        anim.Play("explode");
    }
    public void desSelf()
    {
        if (anim.Animation == "explode")
        {
            QueueFree();
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        if (canMove)
        {
            GlobalPosition += new Vector2(bltSpd, 0);
        }
        else
        {
            GlobalPosition += new Vector2(0, bltSpd * 0.25f);
        }
    }
}