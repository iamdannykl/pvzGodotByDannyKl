using Godot;
using System;

public partial class pea_bullet : bulletBase
{
    [Export] public AudioStreamPlayer audioStreamPlayer;
    [Export] public float freezeTime;
    zombie_base zombieB;
    void atkZom(Area2D zom)
    {
        audioStreamPlayer.Play();
        zombie_base zombieB = zom.Owner as zombie_base;
        if (isAnimPlayer)
        {
            animPlayer.Play("explode");
            canMove = false;
        }
        if ((zom.Owner as zombie_base).Hp > 0)
        {
            if (btp == BulletType.snowPea)
            {
                if (!zombieB.isFrozen)
                {
                    baseCard.playFreeze();
                }
                Timer timer = zombieB.GetNode<Timer>("freezeTime");
                timer.WaitTime = freezeTime;
                zombieB.GetNode<Sprite2D>("Sprite2D").Modulate = new Color("25a2e3");
                zombieB.currrentSpd = zombieB.spd * 0.5f;
                zombieB.isFrozen = true;
                timer.Start();
            }
            zombieB.Hp -= 1;
        }
    }
}
