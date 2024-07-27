using Godot;
using System;
using System.Diagnostics;

public partial class baseCard : Area2D
{
    [Export] public AnimatedSprite2D anim;
    public override void _Ready()
    {
        base._Ready();
        anim.SpeedScale = 0;
        anim.Connect("frame_changed", new Callable(this, nameof(FrameChanged)));
    }
    public void placed()
    {
        anim.SpeedScale = 1;
    }
    public virtual void FrameChanged()
    {
        if (anim.Animation == "attack")
        {
            /* if (anim.Frame == 2)
            {
                GD.Print(anim.Frame);
            } */
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
