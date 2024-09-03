using Godot;
using System;
using System.Diagnostics;

public partial class baseCard : Area2D
{
    [Export] public AnimatedSprite2D anim;
    [Export] public int attackFrame;
    [Export] public int hp;
    [Export] public BulletType bulletType;
    [Export] public RayCast2D rayCast2D;
    [Export] public bool noAtkPlant;
    [Export] public AnimationPlayer animPlayer;
    [Export] public bool isAnimationPlayer;
    [Export] public AudioStreamPlayer 种土上声音;
    [Export] public AudioStreamPlayer 种水上声音;
    public bool isplanted;
    public override void _Ready()
    {
        base._Ready();
        if (!isAnimationPlayer)
        {
            anim.SpeedScale = 0;
            //rayCast2D = GetNode<RayCast2D>("RayCast2D");
            anim.Connect("frame_changed", new Callable(this, nameof(FrameChanged)));
        }
        else
        {
            animPlayer.SpeedScale = 0;
        }
    }
    public override void _Process(double delta)
    {
        if (!noAtkPlant && isplanted)
        {
            if (rayCast2D.IsColliding())
            {
                anim.Play("attack");
            }
            else
            {
                anim.Play("idle");
            }
        }
    }
    public virtual void placed(bool isWaterPlant)
    {
        if (!isAnimationPlayer)
        {
            anim.SpeedScale = 1;
        }
        else
        {
            animPlayer.SpeedScale = 1;
        }
        if (!isWaterPlant)
        {
            种土上声音.Play();
        }
        else
        {
            种水上声音.Play();
        }
        isplanted = true;
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
