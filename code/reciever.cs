using Godot;
using System;

public partial class reciever : Label
{
    public guanQiaType thisGuanQiaType;
    public int sunNum;
    public int RcHangShu;
    public int RcmapIndex;
    public PackedScene mapScene;
    public Node2D map2d;
    public void recieveData(guanQiaType guanQiaType, int sunOriginalNum, int mapIndex, Node nextScene)
    {
        thisGuanQiaType = guanQiaType;
        sunNum = sunOriginalNum;
        RcmapIndex = mapIndex;
        if (RcmapIndex == 0 || RcmapIndex == 1 || RcmapIndex == 4)
        {
            RcHangShu = 5;
        }
        else if (RcmapIndex == 2 || RcmapIndex == 3)
        {
            RcHangShu = 6;
        }
        GD.Print(guanQiaType);
        switch (guanQiaType)
        {
            case guanQiaType.grassDay:
                mapScene = GD.Load<PackedScene>("res://anim/backGround/grass.tscn");
                break;
            case guanQiaType.grassNight:
                //mapScene = GD.Load<PackedScene>();
                break;
            case guanQiaType.poolDay:
                mapScene = GD.Load<PackedScene>("res://Scene/pool_day.tscn");
                break;
            case guanQiaType.poolNight:
                //mapScene = GD.Load<PackedScene>();
                break;
            case guanQiaType.roof:
                //mapScene = GD.Load<PackedScene>();
                break;
        }
        GD.Print(mapScene.ResourcePath);
        map2d = mapScene.Instantiate() as Node2D;
        GD.Print(map2d.GlobalPosition);
        map2d.GlobalPosition = new Vector2(0, 0);
        //nextScene.AddChild(map2d);
        GetTree().Root.AddChild(map2d);
        GD.Print(map2d.GlobalPosition);
    }
}

