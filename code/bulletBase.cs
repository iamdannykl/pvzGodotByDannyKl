using Godot;
using System;
public partial class bulletBase : Area2D
{
    public AnimatedSprite2D anim;
    [Export] public BulletType btp;
    [Export] public AnimationPlayer animPlayer;
    [Export] public bool isAnimPlayer;
    [Export] public float bltSpd;
    [Export] public bool noExplode;
    public baseCard baseCard;
    public bool canMove = true;
    public override void _Ready()
    {
        if (!isAnimPlayer)
        {
            anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            anim.Connect("animation_finished", new Callable(this, nameof(desSelf)));
        }
        ZIndex = 4;
    }
    public void desSelf()
    {
        if (noExplode)
        {
            if (anim.Animation == "idle")
            {
                canMove = false;
                realDes();
            }
            return;
        }
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