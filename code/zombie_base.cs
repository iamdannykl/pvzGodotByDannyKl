using Godot;
using System;

public partial class zombie_base : CharacterBody2D
{
    [Export] public float spd;
    public bool isUpdt;
    public Vector2 weiZhi;
    RayCast2D rayCast;
    public bool isEat;
    stateMC zhuangTaiJi;
    [Export] public ZomType zomType;
    private AnimationTree _animationTree;
    [Export] public int hp;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                zhuangTaiJi.isDead = true;
                GetNode<Area2D>("Area2D").QueueFree();
            }
            if (hp <= 5)
            {
                zhuangTaiJi.isDb = true;
            }
        }
    }
    void stopMove()
    {
        isUpdt = false;
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
        //_animationTree = GetNode<AnimationTree>("AnimationTree");

        //animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        //animationPlayer.SpeedScale = 0.35f;
        //_animationTree.Set("parameters/StateMachine/conditions/isBegin", true);
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
    public void placed()
    {
        isUpdt = false;
    }
    public void crtIt()
    {
        isUpdt = true;
        zhuangTaiJi = GetNode<stateMC>("AnimationTree");
        rayCast = GetNode<RayCast2D>("RayCast2D");
        zhuangTaiJi.isBegin = true;
    }
}
