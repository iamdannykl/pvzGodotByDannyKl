using Godot;
using System;
[Tool]
public partial class sortTheCard : Node2D
{
	// Called when the node enters the scene tree for the first time.
	Vector2 oriPos;
	int xJianJu;
	int yJianJu;
	[Export] public Node2D yx;
	Vector2I hangLieNum = new Vector2I(8, 6);
	[Export]
	public Vector2I HangLieNum
	{
		get => hangLieNum;
		set
		{
			if (!Engine.IsEditorHint())
			{
				return;
			}
			Vector2 Size = yx.GlobalPosition - GlobalPosition;
			xJianJu = (int)(Size.X / value.X);
			yJianJu = (int)(Size.Y / value.Y);
			oriPos = GlobalPosition + /* new Vector2(xJianJu, yJianJu) / 2; */new Vector2(0, yJianJu);
			int i = 0;
			foreach (Node2D item in GetChildren())
			{
				item.GlobalPosition = oriPos + new Vector2(xJianJu * (i % value.X) - item.GetChild<clickButton>(0).UIpianYI.X, yJianJu * (i / value.X) - item.GetChild<clickButton>(0).UIpianYI.Y);
				i++;
			}
			hangLieNum = value;
		}
	}
	public bool hasSort;
	[Export]
	public bool HasSort
	{
		get => hasSort;
		set
		{
			hasSort = value;
		}
	}
}
