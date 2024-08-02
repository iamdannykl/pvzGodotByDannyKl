using Godot;
using System;
using System.Collections;


public partial class sun : Area2D
{
    bool isCollected;
    Vector2 sunLOGOpos;
    [Export] public float spd;
    [Export] public int sunValue;
    Vector2 direction;
    AudioStreamPlayer audioStreamPlayer;

    bool canFly;
    public override void _Ready()
    {
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (canFly)
        {
            flyToZS();
        }
    }
    void desSelf()
    {
        QueueFree();
    }
    void collectSun()
    {
        if (!isCollected)
        {
            isCollected = true;
            AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            animationPlayer.Stop(true);
            Sprite2D sunLOGO = GetTree().CurrentScene.GetNode<Sprite2D>("Camera2D/CardUI/SunClct");
            sunLOGOpos = sunLOGO.GlobalPosition;
            direction = (sunLOGOpos - GlobalPosition).Normalized();
            canFly = true;
            audioStreamPlayer.Play();
            GD.Print("clicked!");
        }
    }
    void flyToZS()
    {
        GlobalPosition += direction * spd;
    }
    void _on_area_2d_mouse_entered()
    {
        if (danli.Instance.isAnXia)
        {
            collectSun();
        }
    }
    void _on_area_2d_input_event(Node viewport, InputEvent @event, int idx)
    {
        if (@event.IsActionPressed("clickIt"))
        {
            collectSun();
        }
    }
}
