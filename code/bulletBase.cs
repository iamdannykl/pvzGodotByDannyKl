using Godot;
using System;
public partial class bulletBase : Area2D
{
    AnimatedSprite2D anim;
    [Export] public BulletType btp;
    [Export] public AnimationPlayer animPlayer;
    [Export] public bool isAnimPlayer;
    [Export] public float bltSpd;
    public bool canMove = true;
    public override void _Ready()
    {
        if (!isAnimPlayer)
        {
            anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            anim.Connect("animation_finished", new Callable(this, nameof(desSelf)));
            Connect("area_entered", new Callable(this, nameof(hitZom)));
        }
        ZIndex = 4;
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
            Visible = false;
            CollisionMask = 0;
            GetNode<Timer>("Timer").Start();
        }
    }
    void realDes()
    {
        QueueFree();
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