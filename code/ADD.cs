using Godot;
using System;

public partial class ADD : Button
{
    [Export] public Control create;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, "ADDitems"));
    }
    void ADDitems()
    {
        create.Visible = true;
    }
}
