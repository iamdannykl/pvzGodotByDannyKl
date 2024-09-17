using Godot;
using System;

public partial class CherryBaoDan : baseCard
{
    public override void explodeIt()
    {
        animPlayer.Play("smoke");
        gridS.plantsOnThisGrid.Remove(this);
    }
}
