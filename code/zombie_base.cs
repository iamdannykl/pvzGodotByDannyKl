using Godot;
using System;

public partial class zombie_base : CharacterBody2D
{
    [Export] public float spd;
    public bool isUpdt;
    public Vector2 weiZhi;
    RayCast2D rayCast;
    public bool isEat;
    public int hangNum;
    stateMC zhuangTaiJi;
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
    }
    void desSelf()
    {
        QueueFree();
    }
    //AnimationPlayer animationPlayer;
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
    }

    public override void _Process(double delta)
    {
        if (isUpdt)
        {
            if (!IsEat)
            {
                Velocity = new Vector2(-spd, 0);
                MoveAndSlide();
            }
            if (!(!zhuangTaiJi.isEat && zhuangTaiJi.isDead) && rayCast.IsColliding())
            {
                baseCard plant = rayCast.GetCollider() as baseCard;
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
    public void crtIt()
    {
        isUpdt = true;
        zhuangTaiJi = GetNode<stateMC>("AnimationTree");
        rayCast = GetNode<RayCast2D>("RayCast2D");
        zhuangTaiJi.isBegin = true;
    }
}
