using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class GridSys : Node2D
{
	public static GridSys Instance;
	public int hangShu;
	public List<hang> hangList = new List<hang>();
	public List<HangType> hangTypes = new List<HangType>();
	public int thisMapIndex;
	public reciever reciever;
	[Export]
	public Node2D zuoxia, youshang;
	public float XjianGe, YjianGe;
	public GridS nowGrd;
	public Vector2 nowPos;
	public bool isOut = true;
	private PackedScene packedScene;
	Vector2 realZX;
	Vector2 distance;
	Vector2 gridPoint;
	int countGD = 0;
	public List<GridS> gridList = new List<GridS>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		packedScene = GD.Load<PackedScene>("res://Prefabs/GDlogo.tscn");
		reciever = GetTree().Root.GetNode<reciever>("GameScene/reciever");
		thisMapIndex = reciever.RcmapIndex;
		hangShu = reciever.RcHangShu;
		XjianGe = (youshang.GlobalPosition.X - zuoxia.GlobalPosition.X) / 8f;
		YjianGe = -(youshang.GlobalPosition.Y - zuoxia.GlobalPosition.Y) / (hangShu - 1);
		GD.Print("XJG:" + XjianGe * 9);
		initHang();
		createHang();
		CreateGridBaseGrid();
		/* GD.Print(zuoxia.GlobalPosition);
		GD.Print(youshang.GlobalPosition);
		GD.Print(GlobalPosition);
		GD.Print(XjianGe + "," + YjianGe); */
		realZX = zuoxia.GlobalPosition - new Vector2(XjianGe / 2, -YjianGe / 2);
		/* Node2D zxnd = packedScene.Instantiate() as Node2D;
		Node2D ysnd = packedScene.Instantiate() as Node2D;
		zxnd.GlobalPosition = zuoxia.GlobalPosition;
		ysnd.GlobalPosition = youshang.GlobalPosition;
		AddChild(zxnd);
		AddChild(ysnd); */
	}
	void initHang()
	{
		if (reciever.thisGuanQiaType == guanQiaType.grassDay || reciever.thisGuanQiaType == guanQiaType.grassNight)
		{
			for (int i = 0; i < hangShu; i++)
			{
				hangTypes.Add(HangType.grass);
			}
		}
		if (reciever.thisGuanQiaType == guanQiaType.poolDay || reciever.thisGuanQiaType == guanQiaType.poolNight)
		{
			hangTypes.Add(HangType.grass);
			hangTypes.Add(HangType.grass);
			hangTypes.Add(HangType.water);
			hangTypes.Add(HangType.water);
			hangTypes.Add(HangType.grass);
			hangTypes.Add(HangType.grass);
		}
		if (reciever.thisGuanQiaType == guanQiaType.roof)
		{
			for (int i = 0; i < hangShu; i++)
			{
				hangTypes.Add(HangType.roof);
			}
		}
	}
	void createHang()
	{
		for (int i = 0; i < hangShu; i++)
		{
			hangList.Add(new hang(hangShu, new Vector2(zuoxia.GlobalPosition.X - XjianGe / 2, zuoxia.GlobalPosition.Y - i * YjianGe), hangTypes[i]));
		}
	}
	public hang GetHangByMouse()//通过鼠标获取行
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		//GD.Print(gridPoint);
		if (gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			gridPoint = new Vector2(-1, -1);
			return null;
		}
		else
		{
			isOut = false;
		}
		if (gridPoint == new Vector2(-1, -1)) return null;
		else
		{
			/* GD.Print("hangList:" + hangList.Count);
			GD.Print("sy:" + (int)gridPoint.Y); */
			//return null;
			return hangList[(int)gridPoint.Y];
		}
	}

	private void CreateGridBaseGrid()
	{
		int num = 0;
		for (int j = 0; j < hangShu; j++)
		{
			for (int i = 0; i < 9; i++)
			{
				//网格体的保存
				//gridList.Add(new GridS(new Vector2(i,j),transform.position+new Vector3(1.48f*i,1.75f*j,0),false,false,num));
				gridList.Add(new GridS(new Vector2(i, j), zuoxia.GlobalPosition + new Vector2(XjianGe * i, -YjianGe * j), false, false, num));
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


	/*public GridS GetGridByMouse()
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
	}*/
	public GridS GetGridByPoint(Vector2 point)
	{
		if (point == new Vector2(-1, -1)) return null;
		else
		{
			nowGrd = gridList[(int)(point.X + 1 + point.Y * 9) - 1];
			//GD.Print(nowGrd.Plant);
			return gridList[(int)(point.X + 1 + point.Y * 9) - 1];
		}
	}
	public void GetPointByMouse()
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		//GD.Print(gridPoint, gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > 4 || gridPoint.Y < 0);
		if (gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			//GD.Print("NULL" + isOut);
			nowPos = new Vector2(-1, -1);
		}
		else
		{
			isOut = false;
			//GD.Print((int)(gridPoint.X + 1 + gridPoint.Y * 9) - 1);
			nowPos = gridPoint;
		}
	}
	public GridS GetGridByMouse()
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		/* GD.Print("-distance.Y:" + -distance.Y); GD.Print("YjianGe" + YjianGe);
		GD.Print("gridPoint:" + gridPoint); */
		//GD.Print(gridPoint, gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > 4 || gridPoint.Y < 0);
		if (gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			nowPos = new Vector2(-1, -1);
			return null;
		}
		else
		{
			isOut = false;
			//GD.Print((int)(gridPoint.X + 1 + gridPoint.Y * 9) - 1);
		}
		if (gridPoint == new Vector2(-1, -1)) return null;
		else
		{
			//GD.Print(nowGrd.Plant);
			return gridList[(int)(gridPoint.X + 1 + gridPoint.Y * 9) - 1];
		}
	}
}
