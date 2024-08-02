using Godot;
using System;

public partial class SunClct : Sprite2D
{
    public static SunClct Instance;
    [Export] public int sunNum;
    Label label;
    public override void _Ready()
    {
        Instance = this;
        label = GetNode<Label>("Label");
        label.Text = sunNum.ToString();
    }
    void getSun(Area2D area2D)
    {
        sunNum += (area2D as sun).sunValue;
        label.Text = sunNum.ToString();
        area2D.GetNode<Timer>("Timer").Start();
        area2D.Visible = false;
    }
}
