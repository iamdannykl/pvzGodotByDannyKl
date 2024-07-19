using Godot;
using System;

public partial class danli : Node2D
{
	public static danli Instance;
	[Export] public Button backToMenu;
	[Export] public Button backToGame;
	[Export] public Button ESC;
	public PackedScene startMenu;
	[Export] public Panel panel;
	[Export] public Button leftSee;
	private clickButton plantCard;
	private zomCard zombieCard;
	public Node2D titleMenu;
	public ConfigFile configFile = new ConfigFile();
	public int abc = 8;
	public zomCard ZombieCard
	{
		get => zombieCard;
		set
		{
			if (zombieCard != null)
			{
				zombieCard.WantPlace = false;
			}
			zombieCard = value;
		}
	}
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

		string configPath = "user://config.cfg"; // 使用"user://"前缀以在用户目录中创建配置文件
		configFile.Load(configPath);
	}
	public void addGq(guanQiaType gtp, int gqs)
	{
		switch (gtp)
		{
			case guanQiaType.grassDay:
				configFile.SetValue("grassDay", "num", gqs);
				break;
			case guanQiaType.grassNight:
				configFile.SetValue("grassNight", "num", gqs);
				break;
			case guanQiaType.poolDay:
				configFile.SetValue("poolDay", "num", gqs);
				break;
			case guanQiaType.poolNight:
				configFile.SetValue("poolNight", "num", gqs);
				break;
			case guanQiaType.roof:
				configFile.SetValue("roof", "num", gqs);
				break;
		}
		configFile.Save("user://config.cfg");
	}
	public int getGq(guanQiaType gtp)
	{
		switch (gtp)
		{
			case guanQiaType.grassDay:
				return (int)configFile.GetValue("grassDay", "num", "0");
			case guanQiaType.grassNight:
				return (int)configFile.GetValue("grassNight", "num", "0");
			case guanQiaType.poolDay:
				return (int)configFile.GetValue("poolDay", "num", "0");
			case guanQiaType.poolNight:
				return (int)configFile.GetValue("poolNight", "num", "0");
			case guanQiaType.roof:
				return (int)configFile.GetValue("roof", "num", "0");
			default:
				return -1;
		}
	}
	void backToStart()
	{
		startMenu = GD.Load<PackedScene>("res://Scene/title.tscn");
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
