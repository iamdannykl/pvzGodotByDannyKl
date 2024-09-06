using Godot;
using System;
using System.Collections.Generic;

public partial class PotatoDl : baseCard
{
	List<zombie_base> zomList = new List<zombie_base>();
	public override void placed(bool isWaterPlant)
	{
		base.placed(isWaterPlant);
		GetNode<Timer>("Timer").Start();
	}
	void maoChu()
	{
		anim.Play("mao");
	}
	void idleAnim()
	{
		if (anim.Animation == "mao")
		{
			anim.Play("idle");
			isZiBao = true;
		}
	}
	void addZom(Area2D zom)
	{
		zomList.Add(zom.GetParent<zombie_base>());
		GD.Print("added!!!");
	}
	public override void explodeIt()
	{
		GD.Print("startEXPLD!!!");
		if (zomList.Count > 0)
		{
			foreach (var zombie in zomList)
			{
				zombie.QueueFree();
			}
		}
		GD.Print("finishEXPLD!!!");
	}
}
