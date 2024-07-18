using Godot;
using System;

public partial class zomCard : TextureButton
{
    [Export] public ZomType zomType;
    public zombie_base zomInsta, zomShadow;
    public hang hang;
    OptionButton naniZR;
    int ZRnum = -1;
    bool wantPlace;
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                zomInsta = resPlantAndZom.Instance.matchZom(zomType).Instantiate() as zombie_base;
                zomShadow = resPlantAndZom.Instance.matchZom(zomType).Instantiate() as zombie_base;
                zomShadow.Modulate = new Color(1, 1, 1, 0.55f);
                zomShadow.GlobalPosition = new Vector2(0, 0);
                zomShadow.Visible = false;
                if (ZRnum == 0)
                {
                    GetTree().Root.GetNode<Node2D>("GameScene/Camera2D/ZR1").AddChild(zomShadow);
                    Vector2 pos = GlobalPosition;
                    zomInsta.GlobalPosition = pos;
                    GetTree().Root.GetNode<Node2D>("GameScene/Camera2D/ZR1").AddChild(zomInsta);
                }
                else if (ZRnum == 1)
                {
                    GetTree().Root.GetNode<Node2D>("GameScene/Camera2D/ZR2").AddChild(zomShadow);
                    Vector2 pos = GlobalPosition;
                    zomInsta.GlobalPosition = pos;
                    GetTree().Root.GetNode<Node2D>("GameScene/Camera2D/ZR2").AddChild(zomInsta);
                }
                else if (ZRnum == 2)
                {
                    GetTree().Root.GetNode<Node2D>("GameScene/Camera2D/ZR3").AddChild(zomShadow);
                    Vector2 pos = GlobalPosition;
                    zomInsta.GlobalPosition = pos;
                    GetTree().Root.GetNode<Node2D>("GameScene/Camera2D/ZR3").AddChild(zomInsta);
                }
                else
                {
                    WantPlace = false;
                }
                /* GetTree().Root.AddChild(zomShadow);
                Vector2 pos = GlobalPosition;
                zomInsta.GlobalPosition = pos;
                GetTree().Root.AddChild(zomInsta); */
            }
            else
            {
                if (zomInsta != null)
                {
                    zomInsta.QueueFree();
                    zomInsta = null;
                }
            }
        }
    }
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(placeZom)));
        naniZR = GetTree().Root.GetNode<OptionButton>("GameScene/Camera2D/zomCardUI/Panel/zhenRong");
        naniZR.Connect("item_selected", new Callable(this, nameof(xuanQuZR)));
    }
    void xuanQuZR(int index)
    {
        ZRnum = index;
    }
    void placeZom()
    {
        if (!WantPlace)
        {
            danli.Instance.ZombieCard = this;
            WantPlace = true;
        }
        else
        {
            WantPlace = false;
        }
    }
    void plantZom()
    {
        if (Input.IsActionJustReleased("clickIt") && GridSys.Instance.isOut == false)//鼠标松开触发
        {
            zomInsta.QueueFree();
            zomInsta = null;
            zomShadow.weiZhi = zomShadow.GlobalPosition - GridSys.Instance.hangList[0].hangTou;
            zomShadow.Modulate = new Color(1, 1, 1, 1);
            zomShadow.GlobalPosition = new Vector2(GetGlobalMousePosition().X, hang.hangTou.Y);
            zomShadow.placed();
            WantPlace = false;
        }
    }
    public override void _Process(double delta)
    {
        if (WantPlace && zomInsta != null)
        {
            zomInsta.GlobalPosition = GetGlobalMousePosition();//跟随鼠标位置
            hang = GridSys.Instance.GetHangByMouse();
            if (hang != null)
            {
                zomShadow.Visible = true;
                zomShadow.GlobalPosition = new Vector2(GetGlobalMousePosition().X, hang.hangTou.Y);
                plantZom();
            }
            else
            {
                zomShadow.Visible = false;
            }
        }
    }
}
