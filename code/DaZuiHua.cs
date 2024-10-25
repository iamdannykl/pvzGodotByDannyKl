using Godot;
using System;

public partial class DaZuiHua : baseCard
{
	bool canSlayZom = true;
	Islayed targetZom;
	[Export] public Timer timer;
	public override void _Process(double delta)
	{
		if (!noAtkPlant && isplanted && canSlayZom)
		{
			if (rayCast2D.IsColliding())
			{
				canSlayZom = false;
				targetZom = (rayCast2D.GetCollider() as Area2D).GetOwner<Islayed>();
				animPlayer.Play("yao");
			}
		}
	}
	public void yaoZom()
	{
		if (targetZom != null)
		{
			targetZom.IbeSlayed();
		}
	}
	public void jue()
	{
		timer.Start();
		animPlayer.Play("jue");
	}
	public void huiFu()
	{
		animPlayer.Play("idle");
		canSlayZom = true;
	}
}
