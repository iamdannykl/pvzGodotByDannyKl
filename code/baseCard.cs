using Godot;
using System;
using System.Diagnostics;

public partial class baseCard : Area2D
{
    [Export] public AnimatedSprite2D anim;
    [Export] public int attackFrame;
    [Export] public BulletType bulletType;
    [Export] public RayCast2D rayCast2D;
    public override void _Ready()
    {
        base._Ready();
        anim.SpeedScale = 0;
        //rayCast2D = GetNode<RayCast2D>("RayCast2D");
        anim.Connect("frame_changed", new Callable(this, nameof(FrameChanged)));
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (rayCast2D.IsColliding())
        {
            anim.Play("attack");
        }
        else
        {
            anim.Play("idle");
        }
    }
    public void placed()
    {
        anim.SpeedScale = 1;
    }
    public virtual void FrameChanged()
    {
        if (anim.Animation == "attack")
        {
            if (anim.Frame == attackFrame)
            {
                bulletBase blt = resPlantAndZom.Instance.matchBullet(bulletType).Instantiate<bulletBase>();
                blt.GlobalPosition = GetNode<Node2D>("pos").GlobalPosition;
                GetTree().CurrentScene.AddChild(blt);
            }
        }
        if (anim.Animation == "idle")
        {
            /* if (anim.Frame == 3)
            {
                GD.Print(anim.Frame);
            } */
        }
    }
}
