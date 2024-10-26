using Godot;
using System;
public partial class DpgBlt : bulletBase
{
    void atkZom(Area2D zom)
    {
        if (!canMove) return;
        Zombie_base zombieB = zom.Owner as Zombie_base;
        if ((zom.Owner as Zombie_base).Hp > 0)
        {
            zombieB.Hp -= 1;
        }
    }
}
