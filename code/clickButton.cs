using Godot;
using System;

public partial class clickButton : TextureButton
{
    public PackedScene plantPrefab;
    public baseCard plantInstan;
    public baseCard shadow;
    [Export] public PlantType plantType;
    [Export] public int sunCost;
    [Export] public Vector2 offset;
    [Export] public Vector2 heYeOffSet;
    [Export] public float cd;
    [Export] public bool isShuiSheng;
    [Export] public bool isTaoLei;
    [Export] public bool 是否为咖啡豆类;
    TextureProgressBar mask;
    public bool isOn;
    public bool isAndroidMode;
    Vector2 bili;
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
                plantInstan.ZIndex = 7;
                plantInstan.CollisionLayer = 0;
                shadow = plantPrefab.Instantiate() as baseCard;
                shadow.ZIndex = 2;
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
    public void deleteShadow()
    {
        if (shadow != null)
        {
            shadow.QueueFree();
            shadow = null;
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
        Connect("button_down", new Callable(this, nameof(_on_button_down)));
        Connect("mouse_entered", new Callable(this, nameof(mouse_entered)));
        Connect("mouse_exited", new Callable(this, nameof(mouse_exited)));
        bili = Scale;
    }
    void mouse_entered()
    {
        GD.Print("mouse has entered!");
        isOn = true;
        if (sunEnough && coldEnough)
        {
            Scale = bili * 1.1f;
        }
        if (OS.GetName() != "Android" && Input.IsActionPressed("clickIt") && danli.Instance.PlantCard != this)
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
    }
    void mouse_exited()
    {
        GD.Print("mouse has exited!");
        if (plantInstan != null && Input.IsActionPressed("clickIt"))
        {
            isAndroidMode = true;
        }
        isOn = false;
        Scale = bili;
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
        GD.Print("upup!");
        if (isAndroidMode && isOn)
        {
            WantPlace = false;
            isAndroidMode = false;
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
            isAndroidMode = false;
            coldEnough = false;
            CDEnter();
            plant.QueueFree();
            plantInstan = null;
            shadow.Monitorable = true;
            shadow.Monitoring = true;
            shadow.ZIndex = 1;
            shadow.CollisionLayer = 1;
            isplanted = true;
            shadow.Modulate = new Color(1, 1, 1, 1);
            shadow.gridS = grid;
            if (grid.plantsOnThisGrid.Count == 0)
            {
                shadow.GlobalPosition = grid.Position - offset;
            }
            else
            {
                /* shadow.GlobalPosition = grid.plantsOnThisGrid[grid.plantsOnThisGrid.Count - 1].GlobalPosition; */
                shadow.GetParent<scene>().RemoveChild(shadow);
                grid.plantsOnThisGrid[grid.plantsOnThisGrid.Count - 1].GetNode<Sprite2D>("Sprite2D").AddChild(shadow);
                shadow.Position = heYeOffSet;
            }
            shadow.placed(isShuiSheng);
            GD.Print("gd:" + grid);
            GD.Print("gdp:" + grid.plantsOnThisGrid);
            grid.plantsOnThisGrid.Add(shadow);
            WantPlace = false;
            if (plantType == PlantType.heYe)
            {
                grid.isHeYe = true;
            }
            SunClct.Instance.SunNum -= sunCost;
        }
    }
    public override void _Process(double delta)
    {
        CurrentSun = SunClct.Instance.SunNum;
        if (WantPlace && plantInstan != null)
        {
            plantInstan.GlobalPosition = GetGlobalMousePosition();//跟随鼠标位置
            grid = GridSys.Instance.GetGridByMouse();
            if ((grid != null) && (
            (grid.gtp == HangType.water && isShuiSheng && !grid.isHeYe) ||
            (grid.gtp == HangType.water && !isShuiSheng && grid.isHeYe && grid.plantsOnThisGrid.Count == 1) ||
            (grid.gtp == HangType.grass && !isShuiSheng && grid.plantsOnThisGrid.Count == 0)
            ))
            {
                shadow.Visible = true;
                shadow.GlobalPosition = grid.Position - offset;
                PlantIt(plantInstan);
            }
            else
            {
                shadow.Visible = false;
            }
        }
    }

}
