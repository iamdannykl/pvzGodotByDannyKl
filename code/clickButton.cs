using Godot;
using System;

public partial class clickButton : TextureButton
{
    public PackedScene plantPrefab;
    public baseCard plantInstan;
    public baseCard shadow;
    [Export] public PlantType plantType;
    [Export] public int sunCost;
    [Export] public float cd;
    TextureProgressBar mask;
    bool sunEnough;
    bool coldEnough = true;
    bool isplanted = false;
    int currentSun;
    private bool wantPlace;
    private bool canPlace;
    public GridS grid;
    float currentTimeInCD;
    public int CurrentSun
    {
        get => currentSun;
        set
        {
            if (currentSun == value) return;
            currentSun = value;
            if (currentSun >= sunCost)
            {
                sunEnough = true;
                SelfModulate = new Color(1, 1, 1, 1);
            }
            else
            {
                sunEnough = false;
                SelfModulate = new Color(0.65f, 0.65f, 0.65f, 1);
            }
        }
    }
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                plantPrefab = resPlantAndZom.Instance.matchPlant(plantType);
                plantInstan = plantPrefab.Instantiate() as baseCard;
                shadow = plantPrefab.Instantiate() as baseCard;
                shadow.Modulate = new Color(1, 1, 1, 0.55f);
                shadow.GlobalPosition = new Vector2(0, 0);
                shadow.Visible = false;
                GetTree().CurrentScene.AddChild(shadow);
                Vector2 pos = GlobalPosition;
                plantInstan.GlobalPosition = pos;
                GetTree().CurrentScene.AddChild(plantInstan);
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
        mask = GetNode<TextureProgressBar>("TextureProgressBar");
        /* GD.Load<PackedScene>("res://Prefabs/repeater.tscn"); */
    }
    void _on_button_up()
    {
        if (!WantPlace)
        {
            if (sunEnough && coldEnough)
            {
                GD.Print("button pressed!" + danli.Instance.tst(danli.Instance.abc));
                danli.Instance.PlantCard = this;
                GD.Print("danliFns");            //生成一个plant
                WantPlace = true;
            }
        }
        else
        {
            WantPlace = false;
        }
    }
    private void CDEnter()
    {
        mask.Value = 1;
        CalCD();
    }
    async void CalCD()//携程计算冷却时间
    {
        float calCD = 1 / cd * 0.1f;
        currentTimeInCD = cd;
        while (currentTimeInCD >= 0)
        {
            await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
            mask.Value -= calCD;
            currentTimeInCD -= 0.1f;
        }
        coldEnough = true;
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
                coldEnough = false;
                CDEnter();
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
                SunClct.Instance.SunNum -= sunCost;
            }
        }
    }
    public override void _Process(double delta)
    {
        CurrentSun = SunClct.Instance.SunNum;
        if (WantPlace && plantInstan != null)
        {
            plantInstan.GlobalPosition = GetGlobalMousePosition();//跟随鼠标位置
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
