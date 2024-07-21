using Godot;
using System;

public partial class createZom : Sprite2D
{
    reciever JieShouQi;
    public saveContent loadData;
    public override void _Ready()
    {
        base._Ready();
    }
    public void _on_timer_timeout()
    {
        loadData = JieShouQi.gqData;
    }
}
