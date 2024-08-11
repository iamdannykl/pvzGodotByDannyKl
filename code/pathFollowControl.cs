using Godot;
using System;

public partial class pathFollowControl : PathFollow2D
{
    public void changeIt(float value)
    {
        ProgressRatio = value;
    }
}
