using Godot;
using System;

public partial class kaiShi : TextureButton
{
    [Export] public Control start;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(openCreateWindow)));
    }
    void openCreateWindow()
    {
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
        start.GetNode<Button>("Panel/ADD").Visible = false;
        start.GetNode<Label>("Panel/MODE").Text = "PlayMode";
        start.Visible = true;
    }
}
