using Godot;
using System;

public partial class quit : Button
{
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(quitGame)));
    }
    void quitGame()
    {
        GetTree().Quit();
    }
}
