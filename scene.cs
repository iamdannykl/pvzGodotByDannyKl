using Godot;
using System;

public partial class scene : Node2D
{
    public static scene Instance;
    public override void _Ready()
    {
        Instance = this;
    }
    public void xianShiUI(playMode InMode)
    {
        if (InMode == playMode.player)
        {
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/CardUI").Visible = true;
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/zomCardUI").Visible = false;
        }
        else if (InMode == playMode.edit)
        {
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/CardUI").Visible = false;
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/zomCardUI").Visible = true;
        }
    }
}
