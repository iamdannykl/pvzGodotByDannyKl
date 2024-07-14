using Godot;
using System;
using System.Diagnostics;

public partial class openPage : TextureButton
{
    [Export] public string url;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(openMyPage)));
    }
    void openMyPage()
    {
        GD.Print(url);
        OS.ShellOpen(url);
    }
}
