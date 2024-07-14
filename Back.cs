using Godot;
using System;

public partial class Back : Button
{
    [Export] public Control start;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(closeCreateWindow)));
    }
    void closeCreateWindow()
    {
        start.Visible = false;
    }
}
