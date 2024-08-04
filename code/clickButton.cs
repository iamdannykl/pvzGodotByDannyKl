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
                plantInstan.Monitorable = false;
                plantInstan.Monitoring = false;
                plantInstan.CollisionLayer = 0;
                shadow = plantPrefab.Instantiate() as baseCard;
                shadow.Modulate = new Color(1, 1, 1, 0.55f);
                shadow.GlobalPosition = new Vector2(0, 0);
                shadow.Visible = false;
                /* shadow.Monitorable = false;
                shadow.Monitoring = false; */
                shadow.CollisionLayer = 0;
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
        //Connect("button_down", new Callable(this, nameof(_on_button_down)));
        Connect("button_up", new Callable(this, nameof(_on_button_up)));
    }
    void _on_button_down()
    {
        if (!WantPlace)
        {
            if (sunEnough && coldEnough)
            {
                danli.Instance.PlantCard = this;
                WantPlace = true;
            }
        }
        else
        {
            WantPlace = false;
        }
    }
    void _on_button_up()
    {
        if (!WantPlace)
        {
            if (sunEnough && coldEnough)
            {
                danli.Instance.PlantCard = this;
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
            Timer timer = GetNode<Timer>("Timer");
            timer.Start();
            await ToSignal(timer, "timeout");
            mask.Value -= calCD;
            currentTimeInCD -= 0.1f;
        }
        coldEnough = true;
    }
    public void PlantIt(baseCard plant)
    {
        if (Input.IsActionJustReleased("clickIt") && GridSys.Instance.isOut == false)//鼠标松开触发
        {
            if (!grid.Plant)
            {
                coldEnough = false;
                CDEnter();
                plant.QueueFree();
                plantInstan = null;
                shadow.Monitorable = true;
                shadow.Monitoring = true;
                shadow.CollisionLayer = 1;
                grid.Plant = true;
                isplanted = true;
                shadow.Modulate = new Color(1, 1, 1, 1);
                shadow.GlobalPosition = grid.Position;
                shadow.placed();
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
