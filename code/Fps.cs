using Godot;
using System;
using System.Globalization;

public partial class Fps : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = "FPS:" + ((int)(1 / delta)).ToString();
	}
}
