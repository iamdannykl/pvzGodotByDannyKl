using Godot;
using System;

public partial class danli : Node2D
{
	public static danli Instance;
	[Export] public Button backToMenu;
	[Export] public Button backToGame;
	[Export] public Button ESC;
	[Export] public PackedScene startMenu;
	[Export] public Panel panel;
	public Node2D titleMenu;
	public int abc = 8;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		backToMenu.Connect("button_up", new Callable(this, nameof(backToStart)));
		backToGame.Connect("button_up", new Callable(this, nameof(backToGameScene)));
		ESC.Connect("button_up", new Callable(this, nameof(pausePanel)));
	}
	void backToStart()
	{
		titleMenu = startMenu.Instantiate() as Node2D;
		GetTree().CurrentScene.QueueFree();
		GetTree().Root.AddChild(titleMenu);
		//titleMenu.GetNode<Label>("Label").Text = "asd";
		GetTree().CurrentScene = titleMenu;
	}
	void backToGameScene()
	{
		panel.Visible = false;
	}
	void pausePanel()
	{
		panel.Visible = true;
	}
	public int tst(int a)
	{
		return a + 2;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
