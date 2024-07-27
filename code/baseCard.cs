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
        //anim.Connect("frame_changed", new Callable(this, "_on_FrameChanged"));
    }
    public void placed()
    {
        anim.SpeedScale = 1;
    }
    void _on_FrameChanged(int index)
    {
        if (anim.Animation == "attack")
        {
            if (index == 2)
            {

            }
        }
    }
}
