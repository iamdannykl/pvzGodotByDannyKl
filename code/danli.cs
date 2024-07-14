using Godot;
using System;

public partial class danli : Node2D
{
	public static danli Instance;
	public int abc = 8;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
	public int tst(int a)
	{
		return a + 2;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
