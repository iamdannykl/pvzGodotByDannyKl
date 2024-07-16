using Godot;
using System;

public partial class clickButton : TextureButton
{
    public PackedScene plantPrefab;
    public baseCard plantInstan;
    public baseCard shadow;
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
                shadow = plantPrefab.Instantiate() as baseCard;
                shadow.Modulate = new Color(1, 1, 1, 0.55f);
                shadow.GlobalPosition = new Vector2(0, 0);
                shadow.Visible = false;
                GetTree().Root.AddChild(shadow);
                Vector2 pos = GlobalPosition;
                plantInstan.GlobalPosition = pos;
                GetTree().Root.AddChild(plantInstan);
            }
            else
            {
                isplanted = false;
                GD.Print("plant:");
                GD.Print(plantInstan != null);
                if (plantInstan != null)
                {
                    plantInstan.QueueFree();
                    plantInstan = null;
                }
            }
        }
    }
    public void initIt()
    {
        WantPlace = false;
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
            danli.Instance.PlantCard = this;
            GD.Print("danliFns");            //生成一个plant
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
                //shadow.QueueFree();
                plant.QueueFree();
                plantInstan = null;
                grid.Plant = true;
                isplanted = true;
                shadow.Modulate = new Color(1, 1, 1, 1);
                shadow.GlobalPosition = grid.Position;
                //plant.GetNode<baseCard>("baseCard").placed();
                shadow.placed();
                /* GD.Print(grid.Position);
                GD.Print(grid.Point);
                GD.Print(GetGlobalMousePosition()); */
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
                shadow.Visible = true;
                shadow.GlobalPosition = grid.Position;
                PlantIt(plantInstan);
            }
            else
            {
                shadow.Visible = false;
            }
        }
    }

}
