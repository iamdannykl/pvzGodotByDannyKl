using Godot;
using System;

public partial class clickButton : TextureButton
{
    public PackedScene plantPrefab;
    public baseCard plantInstan;
    bool isplanted = false;
    private bool wantPlace;
    public GridS grid;
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                plantInstan = plantPrefab.Instantiate() as baseCard;
                Vector2 pos = GlobalPosition;
                plantInstan.GlobalPosition = pos;
                GetTree().Root.AddChild(plantInstan);
            }
            else
            {

            }
        }
    }
    public override void _Ready()
    {
        plantPrefab = GD.Load<PackedScene>("res://Prefabs/repeater.tscn");
    }
    void _on_button_up()
    {
        if (!WantPlace)
        {
            GD.Print("button pressed!" + danli.Instance.tst(danli.Instance.abc));
            //生成一个plant
            WantPlace = true;
        }
    }
    public void PlantIt(baseCard plant)
    {
        //GridSys.Instance.GetPointByMouse();
        if (Input.IsActionJustReleased("clickIt") && GridSys.Instance.isOut == false)//鼠标松开触发
        {
            //GridS gridS = GridSys.Instance.GetGridByPoint(GridSys.Instance.nowPos);
            //GD.Print(gridS.Plant);
            if (!grid.Plant)
            {
                grid.Plant = true;
                isplanted = true;
                plant.GlobalPosition = grid.Position;
                //plant.GetNode<baseCard>("baseCard").placed();
                plant.placed();
                GD.Print(grid.Position);
                GD.Print(grid.Point);
                GD.Print(GetGlobalMousePosition());
                WantPlace = false;
            }
        }
    }
    public override void _Process(double delta)
    {
        /*if (plantInstan != null)
        {
            plantInstan.GlobalPosition = GetGlobalMousePosition();
            PlantIt(plantInstan);
        }*/
        if (WantPlace && plantInstan != null)
        {
            plantInstan.GlobalPosition = GetGlobalMousePosition();
            grid = GridSys.Instance.GetGridByMouse();
            if (grid != null && !grid.Plant)
            {
                //GD.Print(grid.Point);
                PlantIt(plantInstan);
            }
        }
    }

}
