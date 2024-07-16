using Godot;
using System;

public enum playMode
{
    player,
    edit,
    noneMode
}
public partial class reciever : Label
{
    public guanQiaType thisGuanQiaType;
    public playMode playMode;
    public int sunNum;
    public int RcHangShu;
    public int RcmapIndex;
    public PackedScene mapScene;
    public string GQname;
    public Node2D map2d;
    //recieve and init
    public void recieveData(guanQiaType guanQiaType, int sunOriginalNum, int mapIndex, string gqName)
    {
        playMode = playMode.edit;
        GD.Print("RC:" + playMode);
        thisGuanQiaType = guanQiaType;
        sunNum = sunOriginalNum;
        RcmapIndex = mapIndex;
        GQname = gqName;
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

        //SetTheNameOfCurrentQuanQia;
        GetTree().Root.GetNode<Label>("GameScene/Camera2D/guanQiaName").Text = GQname;
    }
}

