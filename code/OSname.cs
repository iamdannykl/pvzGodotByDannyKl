using Godot;
using System;

public partial class OSname : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = OS.GetName();
	}
}
