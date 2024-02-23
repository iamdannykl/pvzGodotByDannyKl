using Godot;
using System;
using System.Collections.Generic;

public partial class GridSys : Node2D
{
	public static GridSys Instance;
	[Export]
	public Node2D zuoxia, youshang;
	public float XjianGe, YjianGe;
	public List<GridS> gridList = new List<GridS>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		XjianGe = (youshang.GlobalPosition.X - zuoxia.GlobalPosition.X) / 8f;
		YjianGe = (youshang.GlobalPosition.Y - zuoxia.GlobalPosition.Y) / 4f;
		CreateGridBaseGrid();
		GD.Print(zuoxia.GlobalPosition);
		GD.Print(youshang.GlobalPosition);
		GD.Print(GlobalPosition);
	}

	private void CreateGridBaseGrid()
	{
		int num = 0;
		for (int j = 0; j < 5; j++)
		{
			for (int i = 0; i < 9; i++)
			{
				//网格体的保存
				//gridList.Add(new GridS(new Vector2(i,j),transform.position+new Vector3(1.48f*i,1.75f*j,0),false,false,num));
				gridList.Add(new GridS(new Vector2(i, j), zuoxia.GlobalPosition + new Vector2(XjianGe * i, YjianGe * j), false, false, num));
				//if(num%2==0)
				//Instantiate(shadow, zuoxia.position + new Vector3(XjianGe * i, YjianGe * j, 0), quaternion.identity);
				num++;
			}
		}
	}
	public Vector2 GetPosByMouse()
	{
		return GetGlobalMousePosition();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(GetGridByMouse().Point);
		//GD.Print(GetPosByMouse());
	}
	public GridS GetGridByMouse()
	{
		//Vector2 point=new Vector2();
		float dis = 100000;
		GridS grid = null;
		Vector2 clickPos = GetPosByMouse();
		int i = 0;
		foreach (GridS pos in gridList)
		{
			if (clickPos.DistanceTo(pos.Position) < dis)
			{
				dis = clickPos.DistanceTo(pos.Position);
				grid = gridList[i];
			}
			i++;
		}
		return grid;
	}
}
