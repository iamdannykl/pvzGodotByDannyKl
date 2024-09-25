using Godot;
using System;

public partial class ADD : Button
{
    [Export] public AnimationPlayer anim;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, "ADDitems"));
    }
    void ADDitems()
    {
        anim.Play("up");
    }
}
