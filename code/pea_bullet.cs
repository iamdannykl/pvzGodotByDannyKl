using Godot;
using System;

public partial class pea_bullet : bulletBase
{
    [Export] public AudioStreamPlayer audioStreamPlayer;
    void atkZom(Area2D zom)
    {
        audioStreamPlayer.Play();
        if ((zom.Owner as zombie_base).Hp > 0)
        {
            (zom.Owner as zombie_base).Hp -= 1;
        }
    }
}
