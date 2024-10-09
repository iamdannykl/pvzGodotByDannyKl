using Godot;
using System;

public partial class zombie_base : CharacterBody2D
{
    [Export] public float spd;
    public float currrentSpd;

    [Export] public AudioStreamPlayer yao;
    public bool isUpdt;
    Random random = new Random();
    // 定义要选择的浮点数数组
    float[] options = { 0.95f, 1.0f, 1.05f };

    public Vector2 weiZhi;
    RayCast2D rayCast;
    public bool isEat;
    public int hangNum;
    stateMC zhuangTaiJi;
    public baseCard plant;
    public baseCard plantAtked;
    public GridS gridAtked;
    bool isEnterTheBoomArea;
    [Export] public ZomType zomType;
    private AnimationTree _animationTree;
    [Export] public int hp;
    HpJianCe hpJianCe;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            zhuangTaiJi.nowJieDuan = hpJianCe.JieDuanJianCe(value);
            hpJianCe.NowJieDuan = zhuangTaiJi.nowJieDuan;
        }
    }
    void stopMove()
    {
        isUpdt = false;
    }
    void deleteCollier()
    {
        GetNode<Area2D>("Area2D").CollisionLayer = 0;
        createZom.Instance.AllZomNum++;
    }
    void desSelf()
    {
        QueueFree();
    }
    //AnimationPlayer animationPlayer;
    void attakePlant()
    {
        yao.Play();
        plantAtked.Hp -= 1;
        //plant.Hp -= 1;
    }
    public bool IsEat
    {
        get => isEat;
        set
        {
            isEat = value;
            zhuangTaiJi.isEat = isEat;
        }
    }
    public override void _Ready()
    {
        hpJianCe = GetNode<HpJianCe>("HpJianCe");
        currrentSpd = spd;
    }

    public override void _Process(double delta)
    {
        if (isUpdt)
        {
            if (!IsEat)
            {
                Velocity = new Vector2(-currrentSpd, 0);
                MoveAndSlide();
            }
            if (!(!zhuangTaiJi.isEat && zhuangTaiJi.isDead) && rayCast.IsColliding())
            {
                GD.PrintT(!(!zhuangTaiJi.isEat && zhuangTaiJi.isDead), rayCast.IsColliding());
                plant = rayCast.GetCollider() as baseCard;
                if (plant != null && plant.isZiBao)
                {
                    plant.explodeIt();
                }
                else
                {
                    if (!plant.isCherryBoom && plant.gridS != null)
                    {
                        gridAtked = plant.gridS;
                        plantAtked = gridAtked.plantsOnThisGrid[gridAtked.plantsOnThisGrid.Count - 1];
                    }
                }
                IsEat = true;
            }
            else
            {
                IsEat = false;
            }
        }
    }
    public void placed(int hang)
    {
        isUpdt = false;
        hangNum = hang;
    }
    public virtual void boomDie(Area2D area2D)
    {
        QueueFree();
        createZom.Instance.AllZomNum++;
    }
    public void crtIt()
    {
        isUpdt = true;
        zhuangTaiJi = GetNode<stateMC>("AnimationTree");
        rayCast = GetNode<RayCast2D>("RayCast2D");
        zhuangTaiJi.isBegin = true;
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationTree.Set("parameters/TimeScale/scale",
        (float)_animationTree.Get("parameters/TimeScale/scale") * options[random.Next(0, options.Length)]);
    }
    public void backToNormalSpd()
    {
        GetNode<Sprite2D>("Sprite2D").Modulate = new Color(1, 1, 1);
        currrentSpd = spd;
    }
}