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
	[Export] public Button leftSee;
	private clickButton plantCard;
	public Node2D titleMenu;
	public int abc = 8;
	public clickButton PlantCard
	{
		get => plantCard;
		set
		{
			/* GD.Print("self");
			GD.Print(value == plantCard);
			GD.Print(plantCard != null);
			GD.Print(value != null); */

			if (plantCard != null)
			{
				GD.Print("next");
				plantCard.WantPlace = false;
			}
			plantCard = value;
		}
	}
	public override void _Ready()
	{
		Instance = this;
		backToMenu.Connect("button_up", new Callable(this, nameof(backToStart)));
		backToGame.Connect("button_up", new Callable(this, nameof(backToGameScene)));
		ESC.Connect("button_up", new Callable(this, nameof(pausePanel)));
		leftSee.Connect("button_up", new Callable(this, nameof(seeLeft)));
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
	void seeLeft()
	{
		if (leftSee.Text == "<=")
		{
			Camera2D.Instance.animationPlayer.Play("leftMove");
			leftSee.Text = "=>";
		}
		else if (leftSee.Text == "=>")
		{
			Camera2D.Instance.animationPlayer.Play("rightMove");
			leftSee.Text = "<=";
		}

	}
	public int tst(int a)
	{
		return a + 2;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
