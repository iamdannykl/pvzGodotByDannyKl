using Godot;
using System;

public partial class sun_flower : baseCard
{
    public override void placed()
    {
        base.placed();
        GetNode<sunCreator>("sunCreator").timer.Start();
    }
}
