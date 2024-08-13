using Godot;
using System;

public partial class BigWaveComing : Label
{
    void fuWei()
    {
        Visible = false;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("RESET");
    }
}
