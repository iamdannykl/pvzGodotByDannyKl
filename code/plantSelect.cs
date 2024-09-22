
using Godot;
using System;
using System.Collections.Generic;
[Tool]
public partial class plantSelect : Node2D
{
	[Export] public int MAXcardNUm;
	/* public GridContainer leftCardCao; */
	public List<clickButton> waitPlantCards = new List<clickButton>();

	Vector2 oriPos;
	float xJianJu;
	float yJianJu;
	Vector2I hangLieNum = new Vector2I(1, 8);
	[Export]
	public Vector2I HangLieNum
	{
		get => hangLieNum;
		set
		{
			if (Engine.IsEditorHint())
			{
				Node2D yx = GetParent<Control>().GetChild<Node2D>(1);
				Vector2 Size = yx.GlobalPosition - GlobalPosition;
				xJianJu = Size.X / value.X;
				yJianJu = Size.Y / value.Y;
				oriPos = GlobalPosition + /* new Vector2(xJianJu, yJianJu) / 2; */new Vector2(0, yJianJu);
				int i = 0;
				foreach (Node2D item in GetChildren())
				{
					item.GlobalPosition = oriPos + new Vector2(xJianJu * (i % value.X), yJianJu * (i / value.X));
					i++;
				}
				hangLieNum = value;
			}
		}
	}
	public bool hasSort;
	[Export]
	public bool HasSort
	{
		get => hasSort;
		set
		{
			sortCard();
			hasSort = value;
		}
	}
	void sortCard()
	{
		GD.Print("sort");
	}
}
