using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
[Tool]
public partial class clickButton : TextureButton
{
    public PackedScene plantPrefab;
    public baseCard plantInstan;
    public baseCard shadow;
    [Export] public Vector2 UIpianYI;
    [Export] public PlantType plantType;
    [Export] public int sunCost;
    /// <summary>
    /// 左上正，右下负
    /// </summary>
    [Export] public Vector2 offset;
    /// <summary>
    /// 右下正，左上负
    /// </summary>
    [Export] public Vector2 heYeOffSet;
    [Export] public float cd;
    [Export] public bool isShuiSheng;
    [Export] public bool isTaoLei;
    [Export] public bool 是否为咖啡豆类;
    [Export] public bool isReadyToPlace;
    TextureProgressBar mask;
    bool canIclick;
    public bool isOn;
    public bool isInList;
    public bool isAndroidMode;
    Vector2 bili;
    bool sunEnough;
    bool coldEnough = true;
    bool isplanted = false;
    int currentSun;
    private bool wantPlace;
    private bool canPlace;
    int leftCaoCardNum;
    public GridS grid;
    plantSelect lftCard;
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
        loadZT();
        //Connect("button_down", new Callable(this, nameof(_on_button_down)));
        Connect("button_up", new Callable(this, nameof(_on_button_up)));
        Connect("button_down", new Callable(this, nameof(_on_button_down)));
        Connect("mouse_entered", new Callable(this, nameof(mouse_entered)));
        Connect("mouse_exited", new Callable(this, nameof(mouse_exited)));
        bili = Scale;
    }
    async void loadZT()
    {
        await Task.Delay(500);
        canIclick = true;
        lftCard = GetTree().CurrentScene.GetNode<plantSelect>("Camera2D/CardUI/zuoKaCao");
    }
    void mouse_entered()
    {
        if (!isReadyToPlace) return;
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
        if (!isReadyToPlace) return;
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
        if (!isReadyToPlace) return;
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
        if (isReadyToPlace)
        {
            GD.Print("upup!");
            if (isAndroidMode && isOn)
            {
                WantPlace = false;
                isAndroidMode = false;
            }
        }
        else
        {
            if (!isInList)
            {
                if (lftCard != null && canIclick && lftCard.waitPlantCards.Count < lftCard.MAXcardNUm)
                {
                    clickButton greyCard = Duplicate() as clickButton;
                    GetParent<Node2D>().AddChild(greyCard);
                    greyCard.Modulate = new Color(0.7f, 0.7f, 0.7f);
                    flyToLeftCardCao();
                }
            }
            else
            {
                //backToSelect();
            }
        }
    }
    async void flyToLeftCardCao()
    {
        isInList = true;
        Vector2 oriPos = GlobalPosition;
        GD.Print("cardGlp1:" + GlobalPosition);
        Visible = false;
        GetParent<Node2D>().RemoveChild(this);
        ZIndex += 1;
        lftCard.waitPlantCards.Add(this);
        leftCaoCardNum = lftCard.waitPlantCards.Count - 1;
        Node2D itemC = lftCard.GetChild<Node2D>(leftCaoCardNum);
        itemC.AddChild(this);
        GlobalPosition = oriPos;
        Visible = true;
        Vector2 tarPos = itemC.GlobalPosition - new Vector2(UIpianYI.X, UIpianYI.Y);
        Vector2 dicVec2 = (tarPos - oriPos).Normalized();
        float dis = (tarPos - oriPos).Length();
        float timeLft = 0.2f;
        float unitDis = calculateUnitDistance(dis, timeLft, 0.0166f);
        while (timeLft >= 0)
        {
            await Task.Delay(16);
            GlobalPosition += unitDis * dicVec2;
            timeLft -= 0.0166f;
        }
        GlobalPosition = tarPos;
        ZIndex -= 1;
        isInList = true;
    }
    async void backToSelect()
    {
        lftCard.waitPlantCards.Remove(this);
        //float unitDis = calculateUnitDistance();
    }
    private void CDEnter()
    {
        mask.Value = 1;
        CalCD();
    }
    public float calculateUnitDistance(float juli, float shijian, float unitShijian)
    {
        return juli / shijian * unitShijian;
    }
    async void CalCD()//异步方法计算冷却时间
    {
        float calCD = calculateUnitDistance(1, cd, 0.1f);//最小单位距离=总距离/总时间*最小单位时间
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
            if (plantType == PlantType.cherryBaoDan)
            {
                shadow.ZIndex = 4;
            }
            else
            {
                shadow.ZIndex = 1;
            }
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
        if (Engine.IsEditorHint())
        {
            return;
        }
        CurrentSun = SunClct.Instance.SunNum;
        if (isReadyToPlace && WantPlace && plantInstan != null)
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
